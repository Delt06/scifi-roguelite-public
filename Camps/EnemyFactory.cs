using System;
using Leopotam.EcsLite;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Camps
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly EnemyStaticData _staticData;

        public EnemyFactory(EnemyStaticData staticData) => _staticData = staticData;

        public EcsPackedEntityWithWorld Spawn(EnemyType enemyType, Vector3 position)
        {
            foreach (var enemyPrefabData in _staticData.Prefabs)
            {
                if (enemyPrefabData.Type != enemyType) continue;

                var entityView = Object.Instantiate(enemyPrefabData.Prefab, position, Quaternion.identity);
                return entityView.GetOrCreateEntity();
            }

            throw new ArgumentException($"Enemy prefab of type {enemyType} not found.");
        }
    }
}