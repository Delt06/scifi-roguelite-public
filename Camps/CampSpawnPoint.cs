using UnityEngine;

namespace Camps
{
    public class CampSpawnPoint : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;

        public EnemyType EnemyType => _enemyType;

        public Vector3 Position => transform.position;

        private void OnDrawGizmos()
        {
            var color = EnemyType.GetColor();
            color.a = 0.25f;
            Gizmos.color = color;
            const float radius = 0.25f;
            Gizmos.DrawSphere(Position + Vector3.up * radius, radius);
        }
    }
}