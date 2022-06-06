using UnityEngine;

namespace Leveling.Energy
{
    public interface ICollectablesFactory
    {
        void CreateEnergy(Vector3 position, int energy);
    }
}