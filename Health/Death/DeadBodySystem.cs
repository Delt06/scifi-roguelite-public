using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Health.Death
{
    public class DeadBodySystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly EcsWorld _world;

        public DeadBodySystem(EcsWorld world)
        {
            _world = world;
            _filter = world.Filter<DeathCommand>()
                .Inc<ViewBackRef>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var deadBodyData = ref _world.NewPackedEntityWithWorld().Add<DeadBodyData>();
                deadBodyData.EntityView = _filter.GetView(i);
            }
        }
    }
}