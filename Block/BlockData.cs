using System;
using UnityEngine;

namespace Block
{
    [Serializable]
    public struct BlockData
    {
        [Min(0f)]
        public float StunDuration;
        [Min(0f)]
        public float TimeToActivate;
    }
}