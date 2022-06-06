using Leopotam.EcsLite;
using Leveling.UI;

namespace Leveling
{
    public abstract class StatsUpdateSystem<TData, TLevelingData> : IEcsRunSystem where TData : struct
    {
        private readonly EcsFilter _filter;
        private readonly LevelingStaticData _levelingStaticData;
        private readonly IPlayerStats _playerStats;


        protected StatsUpdateSystem(EcsWorld world, IPlayerStats playerStats,
            LevelingStaticData levelingStaticData)
        {
            _filter = world.Filter<StatsUpdateRequest>()
                .Inc<TData>()
                .End();
            _levelingStaticData = levelingStaticData;
            _playerStats = playerStats;
        }

        protected abstract Stat Stat { get; }

        public void Run(EcsSystems systems)
        {
            var levelingData = SelectLevelingData(_levelingStaticData);

            foreach (var i in _filter)
            {
                HandleUpdate(_filter, levelingData, i);
            }
        }

        protected int GetLevelIndex() => _playerStats[Stat];

        protected abstract TLevelingData SelectLevelingData(LevelingStaticData levelingStaticData);

        protected abstract void HandleUpdate(EcsFilter filter, in TLevelingData levelingData, int i);
    }
}