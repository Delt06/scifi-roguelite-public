using System;
using UnityEngine;

namespace Health.Damage
{
    [Serializable]
    public struct DamageData
    {
        [Min(0f)]
        public float Damage;
    }
}