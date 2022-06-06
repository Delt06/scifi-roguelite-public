using Health.Damage;
using Leopotam.EcsLite;
using UnityEngine;

namespace Block
{
    public struct BlockEvent
    {
        public EcsPackedEntityWithWorld Blocker;
        public EcsPackedEntityWithWorld Attacker;
        public Vector3 Position;
        public DamageType DamageType;
    }
}