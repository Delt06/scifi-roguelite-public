using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Health.Death
{
    public class DeathEntityDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public DeathEntityDestroySystem(EcsWorld world) =>
            _filter = world.Filter<DeathCommand>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                _filter.Destroy(i);
            }
        }
    }
}