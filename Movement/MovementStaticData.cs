using UnityEngine;

namespace Movement
{
    [CreateAssetMenu]
    public class MovementStaticData : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float _movementThreshold = 0.1f;
        [SerializeField] [Min(0f)] private float _rotationDirectionThreshold = 0.01f;
        [SerializeField] private LayerMask _environmentLayerMask;

        public float MovementThreshold => _movementThreshold;

        public LayerMask EnvironmentLayerMask => _environmentLayerMask;

        public float RotationDirectionThreshold => _rotationDirectionThreshold;
    }
}