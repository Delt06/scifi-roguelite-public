using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace TimeUtils
{
    public class TimerUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public TimerUpdateSystem(EcsWorld world) =>
            _filter = world.Filter<TimerData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var timerData = ref _filter.Get<TimerData>(i);
                timerData.RemainingTime -= Time.deltaTime;
                if (timerData.RemainingTime > 0f) continue;

                _filter.Add<TimerEndedEvent>(i);
                _filter.Del<TimerData>(i);
            }
        }
    }
}