using System;
using DELTation.LeoEcsExtensions.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Camps
{
    [CreateAssetMenu]
    public class EnemyStaticData : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float _campReturnDistance = 15f;
        [SerializeField] [TableList] private EnemyPrefabData[] _prefabs;

        public float CampReturnDistance
        {
            get => _campReturnDistance;
            set => _campReturnDistance = value;
        }

        public EnemyPrefabData[] Prefabs => _prefabs;

        [Serializable]
        public struct EnemyPrefabData
        {
            public EnemyType Type;
            [Required]
            public EntityView Prefab;
        }
    }
}