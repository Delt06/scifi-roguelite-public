using System;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ai
{
    [Serializable]
    public struct FollowData
    {
        [Min(0f)]
        public float DesiredDistance;
        [Min(0f)]
        public float MaxDistanceToLook;

        [Required]
        public Transform VisibilityCheckRayStart;
        [Min(0f)]
        public float VisibilityCheckRayRadius;

        public EcsPackedEntityWithWorld? Target;
    }
}