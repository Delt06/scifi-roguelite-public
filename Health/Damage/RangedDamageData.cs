using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Health.Damage
{
    [Serializable]
    public struct RangedDamageData
    {
        [Required]
        public Transform ShootFrom;
        [Min(0f)]
        public float Radius;
        [Min(0f)]
        public float MaxDistance;
        [Min(0f)]
        public float MaxSpreadAngle;
        [Required]
        public ParticleSystem Trail;
        [Min(0f)]
        public float Impulse;
    }
}