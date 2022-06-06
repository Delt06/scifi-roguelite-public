using Leveling.UI;

namespace Leveling.Raw
{
    public interface IRawPlayerStats
    {
        int this[Stat stat] { get; set; }
        int FreeStatPoints { get; set; }
    }
}