using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Movement.Roll
{
    public class RollRootMotionMultiplierSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public RollRootMotionMultiplierSystem(EcsWorld world) =>
            _filter = world.Filter<RollData>()
                .Inc<RollState>()
                .Inc<MovementData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var movementData = ref _filter.Get<MovementData>(i);
                var rootMotionMultiplier = _filter.Read<RollData>(i).RootMotionMultiplier;
                movementData.RootMotion *= rootMotionMultiplier;
            }
        }
    }
}