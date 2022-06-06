using System;
using DELTation.LeoEcsExtensions.Services;
using DELTation.LeoEcsExtensions.Utilities;
using Leveling.Raw;
using Leveling.UI;

namespace Leveling
{
    public class PlayerStats : IPlayerStats
    {
        private readonly IActiveEcsWorld _activeEcsWorld;
        private readonly IRawPlayerStats _playerStats;

        public PlayerStats(IActiveEcsWorld activeEcsWorld, IRawPlayerStats playerStats)
        {
            _activeEcsWorld = activeEcsWorld;
            _playerStats = playerStats;
        }

        public int this[Stat stat] => _playerStats[stat];

        public int FreeStatPoints
        {
            get => _playerStats.FreeStatPoints;
            private set
            {
                if (_playerStats.FreeStatPoints == value) return;
                _playerStats.FreeStatPoints = value;
                FreeStatPointsChanged?.Invoke();
            }
        }

        public void AddFreeStatPoints(int points)
        {
            FreeStatPoints += points;
        }

        public event Action FreeStatPointsChanged;

        public void TryUpgrade(Stat stat)
        {
            if (FreeStatPoints <= 0) return;
            _playerStats[stat]++;
            FreeStatPoints--;
            NotifyEcsWorld();
        }

        private void NotifyEcsWorld()
        {
            var filter = _activeEcsWorld.World.Filter<StatsReceiverTag>().End();
            foreach (var i in filter)
            {
                filter.GetOrAdd<StatsUpdateRequest>(i);
            }
        }
    }
}