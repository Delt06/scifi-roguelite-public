using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Health.Damage
{
    public class MeleeWeaponBehavior : MonoBehaviour
    {
        [SerializeField] [Required] private BoxCollider _hitBox;
        [SerializeField] [Required] private ParticleSystem _fx;

        public Vector3 Forward => transform.forward;

        public (Vector3 center, Vector3 size, Quaternion orientation) GetHitBoxInfo()
        {
            var hitBox = _hitBox;
            var hitBoxTransform = hitBox.transform;
            var center = hitBoxTransform.TransformPoint(hitBox.center);
            var size = (float3) hitBoxTransform.lossyScale * hitBox.size;
            var orientation = hitBoxTransform.rotation;
            return (center, size, orientation);
        }

        public void PlayFxAt(Vector3 position)
        {
            _fx.transform.position = position;
            _fx.Play();
        }
    }
}