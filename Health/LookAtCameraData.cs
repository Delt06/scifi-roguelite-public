using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Health
{
    [Serializable]
    public struct LookAtCameraData
    {
        [Required]
        public Transform Transform;
    }
}