using UnityEngine;

namespace Health
{
    [CreateAssetMenu]
    public class HealthStaticData : ScriptableObject
    {
        [SerializeField] private LayerMask _charactersLayerMask;
        [SerializeField] private LayerMask _rangedAttackLayerMask;

        public LayerMask CharactersLayerMask => _charactersLayerMask;

        public LayerMask RangedAttackLayerMask => _rangedAttackLayerMask;
    }
}