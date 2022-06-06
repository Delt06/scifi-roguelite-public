using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public struct CharacterControllerData
    {
        [Required]
        public CharacterController CharacterController;
        [HideInEditorMode]
        public Vector3 GravityVelocity;
    }
}