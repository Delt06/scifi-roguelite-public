using DELTation.LeoEcsExtensions.Utilities;
using Health.Damage;
using Leopotam.EcsLite;
using Leveling.UI;

namespace Leveling
{
    public class DamageStatsUpdateSystem : StatsUpdateSystem<DamageData, DamageLevelingData>
    {
        public DamageStatsUpdateSystem(EcsWorld world, IPlayerStats playerStats,
            LevelingStaticData levelingStaticData) : base(world, playerStats, levelingStaticData) { }

        protected override Stat Stat => Stat.Damage;

        protected override DamageLevelingData SelectLevelingData(LevelingStaticData levelingStaticData) =>
            levelingStaticData.Damage;

        protected override void HandleUpdate(EcsFilter filter,
            in DamageLevelingData levelingData, int i)
        {
            ref var damageData = ref filter.Get<DamageData>(i);
            var levelIndex = GetLevelIndex();
            damageData.Damage = levelingData.ComputeForLevelIndex(levelIndex);
        }
    }
}