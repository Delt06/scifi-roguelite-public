using _Shared;
using DELTation.LeoEcsExtensions.Utilities;
using Health.Death;
using Leopotam.EcsLite;

namespace Leveling.Energy
{
    public class PlayerDeathEnergyDropSystem : IEcsRunSystem
    {
        private readonly ICollectablesFactory _collectablesFactory;
        private readonly EcsFilter _filter;
        private readonly IPlayerExperience _playerExperience;

        public PlayerDeathEnergyDropSystem(EcsWorld world,
            IPlayerExperience playerExperience, ICollectablesFactory collectablesFactory)
        {
            _playerExperience = playerExperience;
            _collectablesFactory = collectablesFactory;
            _filter = world.Filter<PlayerTag>()
                .Inc<DeathCommand>()
                .IncTransform()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var droppedEnergy = _playerExperience.Energy;
                _playerExperience.LoseAllEnergy();
                var position = _filter.GetTransform(i).position;
                _collectablesFactory.CreateEnergy(position, droppedEnergy);
            }
        }
    }
}