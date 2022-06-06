using System;
using UnityEngine;

namespace Leveling
{
    [Serializable]
    public struct HealthLevelingData
    {
        [Min(0f)]
        public float BaseHealth;
        [Min(0f)]
        public float HealthPerLevel;
    }
}