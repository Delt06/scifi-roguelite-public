using DELTation.LeoEcsExtensions.Utilities;
using Health;
using Leopotam.EcsLite;
using Leveling.UI;

namespace Leveling
{
    public class HealthStatsUpdateSystem : StatsUpdateSystem<HealthData, HealthLevelingData>
    {
        public HealthStatsUpdateSystem(EcsWorld world, IPlayerStats playerStats,
            LevelingStaticData levelingStaticData) : base(world, playerStats, levelingStaticData) { }

        protected override Stat Stat => Stat.Health;

        protected override HealthLevelingData SelectLevelingData(LevelingStaticData levelingStaticData) =>
            levelingStaticData.Health;

        protected override void HandleUpdate(EcsFilter filter,
            in HealthLevelingData levelingData, int i)
        {
            ref var healthData = ref filter.Modify<HealthData>(i);
            var levelIndex = GetLevelIndex();
            var newMaxHealth = levelingData.ComputeForLevelIndex(levelIndex);
            var ratio = healthData.Health / healthData.MaxHealth;
            healthData.Health = newMaxHealth * ratio;
            healthData.MaxHealth = newMaxHealth;
        }
    }
}