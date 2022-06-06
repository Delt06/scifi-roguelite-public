using Health.Teams;
using Leopotam.EcsLite;
using UnityEngine;

namespace Health.Damage
{
    public struct DealDamageCommand
    {
        public EcsPackedEntityWithWorld Target;
        public Team AttackerTeam;
        public float Damage;
        public Vector3 Impulse;
        public DamageType Type;
        public EcsPackedEntityWithWorld Attacker;
        public Vector3 Position;
    }
}