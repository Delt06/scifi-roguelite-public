using System;
using UnityEngine;

namespace Leveling
{
    [Serializable]
    public struct DamageLevelingData
    {
        [Min(0f)]
        public float BaseDamage;
        [Min(0f)]
        public float DamagePerLevel;
    }
}