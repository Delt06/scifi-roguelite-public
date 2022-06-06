using _Shared;
using DELTation.LeoEcsExtensions.Views.Components;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Camps
{
    public class CampDataView : ComponentView<CampData>
    {
        private void OnDrawGizmos()
        {
            var spawnPoints = StoredComponentValue.SpawnPoints;
            if (spawnPoints == null) return;

            foreach (var spawnPoint in spawnPoints)
            {
                if (!spawnPoint) continue;
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, spawnPoint.transform.position);
            }
        }

        private void OnValidate()
        {
            UpdateSpawnPoints();
        }

        [Button]
        private void UpdateSpawnPoints()
        {
            StoredComponentValue.SpawnPoints = GetComponentsInChildren<CampSpawnPoint>();
        }
#if UNITY_EDITOR


        [MinValue(0f)] [LabelText("Return Distance (SHARED)")] [ShowInInspector]
        private float ReturnDistance
        {
            get => EnemyStaticData().CampReturnDistance;
            set
            {
                var enemyStaticData = EnemyStaticData();
                enemyStaticData.CampReturnDistance = value;
                EditorUtility.SetDirty(enemyStaticData);
                AssetDatabase.SaveAssetIfDirty(enemyStaticData);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var enemyStaticData = EnemyStaticData();
            var radius = enemyStaticData.CampReturnDistance;
            GizmosDrawFlatCircle(transform.position, radius);
        }

        private static EnemyStaticData EnemyStaticData() =>
            StaticDataProvider.FindOrDefault()?.EnemyStaticData;

        private static void GizmosDrawFlatCircle(Vector3 center, float radius)
        {
            var oldMatrix = Gizmos.matrix;
            Gizmos.matrix = oldMatrix * Matrix4x4.Scale(new Vector3(1f, 0f, 1f));
            Gizmos.DrawWireSphere(center, radius);
            Gizmos.matrix = oldMatrix;
        }
#endif
    }
}