using _Shared;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Maps;

namespace Doors
{
    public class DoorTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly EcsFilter _playerFilter;
        private readonly IGateService _gateService;

        public DoorTriggerSystem(EcsWorld world, IGateService gateService)
        {
            _filter = world.Filter<DoorTriggerEnterEvent>()
                .End();
            _playerFilter = world.Filter<PlayerTag>().End();
            _gateService = gateService;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var doorTriggerEnterEvent = _filter.Read<DoorTriggerEnterEvent>(i);
                if (!_playerFilter.Contains(doorTriggerEnterEvent.EnteredEntity)) continue;

                _gateService.Open(doorTriggerEnterEvent.Gate);
            }
        }
    }
}