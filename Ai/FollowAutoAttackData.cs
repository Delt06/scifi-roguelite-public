using System;
using UnityEngine;

namespace Ai
{
    [Serializable]
    public struct FollowAutoAttackData
    {
        [Min(0f)]
        public float MaxDistance;

        public bool SkipWhenBlocking;
    }
}