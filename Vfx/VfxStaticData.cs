using Sirenix.OdinInspector;
using UnityEngine;

namespace Vfx
{
    [CreateAssetMenu]
    public class VfxStaticData : ScriptableObject
    {
        [SerializeField] [Required] private ParticleSystem _blockFxPrefab;
        [SerializeField] [Required] private ParticleSystem _dodgeFxPrefab;

        public ParticleSystem BlockFxPrefab => _blockFxPrefab;

        public ParticleSystem DodgeFxPrefab => _dodgeFxPrefab;
    }
}