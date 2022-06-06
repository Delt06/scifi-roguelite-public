using System;
using JetBrains.Annotations;
using UnityEngine;

namespace _Shared.Utils
{
    public static class ColliderExt
    {
        public static Vector3 GetCenter([NotNull] this Collider collider)
        {
            if (collider == null) throw new ArgumentNullException(nameof(collider));
            var localCenter = collider switch
            {
                BoxCollider boxCollider => boxCollider.center,
                CapsuleCollider capsuleCollider => capsuleCollider.center,
                CharacterController characterController => characterController.center,
                MeshCollider _ => Vector3.zero,
                SphereCollider sphereCollider => sphereCollider.center,
                TerrainCollider _ => Vector3.zero,
                WheelCollider wheelCollider => wheelCollider.center,
                _ => throw new ArgumentOutOfRangeException(nameof(collider)),
            };
            return collider.transform.TransformPoint(localCenter);
        }
    }
}