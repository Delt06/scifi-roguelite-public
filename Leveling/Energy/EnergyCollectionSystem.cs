using _Shared;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Leveling.Energy
{
    public class EnergyCollectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter _collectorsFilter;
        private readonly EcsFilter _filter;
        private readonly IPlayerExperience _playerExperience;

        public EnergyCollectionSystem(EcsWorld world, IPlayerExperience playerExperience)
        {
            _playerExperience = playerExperience;
            _filter = world.Filter<EnergyCollectionCommand>()
                .End();
            _collectorsFilter = world.Filter<PlayerTag>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var collectionCommand = _filter.Read<EnergyCollectionCommand>(i);
                if (collectionCommand.Collectable == null) continue;
                if (!_collectorsFilter.Contains(collectionCommand.Collector)) continue;

                _playerExperience.AddEnergy(collectionCommand.Energy);
                Object.Destroy(collectionCommand.Collectable);
            }
        }
    }
}