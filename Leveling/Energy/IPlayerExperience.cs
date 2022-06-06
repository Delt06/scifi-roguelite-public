using System;

namespace Leveling.Energy
{
    public interface IPlayerExperience
    {
        int LevelIndex { get; }
        int Energy { get; }
        int RequiredEnergyForNextLevel { get; }

        event Action EnergyChanged;

        void AddEnergy(int energy);
        void LoseAllEnergy();
    }
}