using UnityEngine;

namespace Leveling.Energy
{
    public class CollectablesFactory : ICollectablesFactory
    {
        private readonly LevelingStaticData _levelingStaticData;

        public CollectablesFactory(LevelingStaticData levelingStaticData) => _levelingStaticData = levelingStaticData;

        public void CreateEnergy(Vector3 position, int energy)
        {
            var prefab = _levelingStaticData.CollectableEnergyPrefab;
            var energyBehaviour = Object.Instantiate(prefab, position, Quaternion.identity);
            energyBehaviour.Energy = energy;
        }
    }
}