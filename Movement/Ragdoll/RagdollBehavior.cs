using Sirenix.OdinInspector;
using UnityEngine;

namespace Movement.Ragdoll
{
    public class RagdollBehavior : MonoBehaviour
    {
        [SerializeField] [Required] private Collider[] _normalColliders;
        [SerializeField] [Required] private Collider[] _ragdollColliders;
        [SerializeField] [Required] private Rigidbody[] _ragdollRigidbodies;
        [SerializeField] [Required] private Animator _animator;
        [SerializeField] private bool _enabledOnAwake;

        private void Awake()
        {
            Toggle(_enabledOnAwake);
        }

        public void AddImpulse(Vector3 impulse)
        {
            foreach (var ragdollRigidbody in _ragdollRigidbodies)
            {
                ragdollRigidbody.AddForce(impulse, ForceMode.Impulse);
            }
        }

        public void Toggle(bool on)
        {
            foreach (var normalCollider in _normalColliders)
            {
                normalCollider.enabled = !on;
            }

            foreach (var ragdollCollider in _ragdollColliders)
            {
                ragdollCollider.enabled = on;
            }

            foreach (var ragdollRigidbody in _ragdollRigidbodies)
            {
                ragdollRigidbody.isKinematic = !on;
            }

            _animator.enabled = !on;
        }
    }
}