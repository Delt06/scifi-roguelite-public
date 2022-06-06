using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public struct MovementData
    {
        [Min(0f)]
        public float MovementSpeed;
        [HideInEditorMode]
        public Vector3 Direction;
        [Min(0f)]
        public float RotationDampTime;
        [HideInEditorMode]
        public Quaternion RotationAngularVelocity;
        [HideInEditorMode]
        public Vector3 RootMotion;

        [Min(0f)]
        public float AnimationSpeedMultiplier;
    }
}