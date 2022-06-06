using DELTation.LeoEcsExtensions.Utilities;
using Health.Death;
using Leopotam.EcsLite;

namespace Spawning
{
    public class DestroyDeadBodiesOnGameRestartSystem : IEcsRunSystem
    {
        private readonly EcsFilter _commandFilter;
        private readonly EcsFilter _filter;

        public DestroyDeadBodiesOnGameRestartSystem(EcsWorld world)
        {
            _filter = world.Filter<DeadBodyData>()
                .End();
            _commandFilter = world.Filter<GameRestartCommand>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_commandFilter.IsEmpty()) return;

            foreach (var i in _filter)
            {
                var entityView = _filter.Read<DeadBodyData>(i).EntityView;
                entityView.Destroy();
                _filter.Del<DeadBodyData>(i);
            }
        }
    }
}