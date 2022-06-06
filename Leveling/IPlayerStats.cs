using System;
using Leveling.UI;

namespace Leveling
{
    public interface IPlayerStats
    {
        int this[Stat stat] { get; }
        int FreeStatPoints { get; }
        void AddFreeStatPoints(int points);
        event Action FreeStatPointsChanged;

        void TryUpgrade(Stat stat);
    }
}