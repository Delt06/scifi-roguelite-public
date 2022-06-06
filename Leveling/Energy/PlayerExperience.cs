using System;
using Leveling.Raw;
using UnityEngine;

namespace Leveling.Energy
{
    public class PlayerExperience : IPlayerExperience
    {
        private readonly LevelingStaticData _levelingStaticData;
        private readonly IPlayerStats _playerStats;
        private readonly IRawPlayerExperience _rawPlayerExperience;

        public PlayerExperience(IRawPlayerExperience rawPlayerExperience, LevelingStaticData levelingStaticData,
            IPlayerStats playerStats)
        {
            _rawPlayerExperience = rawPlayerExperience;
            _levelingStaticData = levelingStaticData;
            _playerStats = playerStats;
        }

        public int LevelIndex
        {
            get => _rawPlayerExperience.LevelIndex;
            private set => _rawPlayerExperience.LevelIndex = value;
        }

        public int Energy
        {
            get => _rawPlayerExperience.Energy;
            private set
            {
                value = Mathf.Max(0, value);
                if (_rawPlayerExperience.Energy == value) return;
                _rawPlayerExperience.Energy = value;
                EnergyChanged?.Invoke();
            }
        }

        public int RequiredEnergyForNextLevel =>
            (int) _levelingStaticData.EnergyForLevel.Evaluate(LevelIndex + 1);

        public event Action EnergyChanged;

        public void AddEnergy(int energy)
        {
            Energy += energy;

            while (Energy >= RequiredEnergyForNextLevel)
            {
                Energy -= RequiredEnergyForNextLevel;
                LevelIndex++;
                _playerStats.AddFreeStatPoints(1);
            }
        }

        public void LoseAllEnergy() => Energy = 0;
    }
}