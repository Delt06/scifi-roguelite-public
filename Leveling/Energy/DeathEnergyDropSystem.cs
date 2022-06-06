using DELTation.LeoEcsExtensions.Utilities;
using Health.Death;
using Leopotam.EcsLite;
using UnityEngine;

namespace Leveling.Energy
{
    public class DeathEnergyDropSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly IPlayerExperience _playerExperience;

        public DeathEnergyDropSystem(EcsWorld world, IPlayerExperience playerExperience)
        {
            _filter = world.Filter<EnergyDropData>()
                .Inc<DeathCommand>()
                .End();
            _playerExperience = playerExperience;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var energyValueRange = _filter.Read<EnergyDropData>(i).ValueRange;
                var energy = Random.Range(energyValueRange.x, energyValueRange.y);
                _playerExperience.AddEnergy(energy);
            }
        }
    }
}