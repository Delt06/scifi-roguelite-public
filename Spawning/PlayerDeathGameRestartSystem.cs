using _Shared;
using DELTation.LeoEcsExtensions.Components;
using Health.Death;
using Leopotam.EcsLite;
using TimeUtils;

namespace Spawning
{
    public class PlayerDeathGameRestartSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly SpawningStaticData _spawningStaticData;
        private readonly EcsWorld _world;

        public PlayerDeathGameRestartSystem(EcsWorld world, SpawningStaticData spawningStaticData)
        {
            _world = world;
            _filter = world.Filter<DeathCommand>()
                .Inc<PlayerTag>()
                .End();
            _spawningStaticData = spawningStaticData;
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()) return;

            var timerEntity = _world.NewPackedEntityWithWorld();
            timerEntity.Add<TimerData>().RemainingTime = _spawningStaticData.GameRestartAfterPlayerDeathDelay;
            timerEntity.Add<RestartGameWhenTimerEndsTag>();
        }
    }
}