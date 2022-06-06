using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using TimeUtils;

namespace Spawning
{
    public class RestartGameWhenTimerEndsSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly EcsWorld _world;

        public RestartGameWhenTimerEndsSystem(EcsWorld world)
        {
            _world = world;
            _filter = world.Filter<TimerEndedEvent>()
                .Inc<RestartGameWhenTimerEndsTag>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                _world.NewPackedEntityWithWorld().Add<GameRestartCommand>();
                _filter.Destroy(i);
            }
        }
    }
}