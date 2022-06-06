using Leveling.Energy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Leveling
{
    [CreateAssetMenu]
    public class LevelingStaticData : ScriptableObject
    {
        [SerializeField] [InlineProperty] private DamageLevelingData _damage;
        [SerializeField] [InlineProperty] private HealthLevelingData _health;
        [SerializeField] [Required] private CollectableEnergyBehaviour _collectableEnergyPrefab;
        [SerializeField] private AnimationCurve _energyForLevel;

        public DamageLevelingData Damage => _damage;

        public HealthLevelingData Health => _health;

        public CollectableEnergyBehaviour CollectableEnergyPrefab => _collectableEnergyPrefab;

        public AnimationCurve EnergyForLevel => _energyForLevel;
    }
}