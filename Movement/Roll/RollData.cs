using System;
using UnityEngine;

namespace Movement.Roll
{
    [Serializable]
    public struct RollData
    {
        [Min(0f)]
        public float InvincibilityTime;
        [Min(0f)]
        public float RootMotionMultiplier;
    }
}