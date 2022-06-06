using System;
using UnityEngine;

namespace Attack
{
    [Serializable]
    public struct AttackData
    {
        [Min(0f)]
        public float Duration;
        [Range(0f, 1f)]
        public float ActivationTime;

        [Min(0f)]
        public float Cooldown;
    }
}