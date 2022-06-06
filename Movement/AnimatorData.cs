using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public struct AnimatorData
    {
        [Required]
        public Animator Animator;
    }
}