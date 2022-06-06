using Leopotam.EcsLite;
using UnityEngine;

namespace Camps
{
    public interface IEnemyFactory
    {
        EcsPackedEntityWithWorld Spawn(EnemyType enemyType, Vector3 position);
    }
}