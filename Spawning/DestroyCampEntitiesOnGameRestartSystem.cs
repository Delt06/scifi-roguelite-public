using Camps;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Spawning
{
    public class DestroyCampEntitiesOnGameRestartSystem : IEcsRunSystem
    {
        private readonly EcsFilter _commandFilter;
        private readonly EcsFilter _filter;

        public DestroyCampEntitiesOnGameRestartSystem(EcsWorld world)
        {
            _filter = world.Filter<CampRef>().Inc<ViewBackRef>().End();
            _commandFilter = world.Filter<GameRestartCommand>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (EcsFilterExtensions.IsEmpty(_commandFilter)) return;

            foreach (var i in _filter)
            {
                _filter.GetView(i).Destroy();
            }
        }
    }
}