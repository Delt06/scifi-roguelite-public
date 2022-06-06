using System;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public struct LookAtClosestEnemyData
    {
        public Vector3 CheckCenterOffset;
        [Min(0f)]
        public float CheckRadius;
    }
}