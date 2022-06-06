using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Vfx;

namespace Movement.Roll
{
    public class DodgeVfxSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly IVfxFactory _vfxFactory;

        public DodgeVfxSystem(EcsWorld world, IVfxFactory vfxFactory)
        {
            _vfxFactory = vfxFactory;
            _filter = world.Filter<DodgeEvent>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var position = _filter.Read<DodgeEvent>(i).Position;
                _vfxFactory.Create(sd => sd.DodgeFxPrefab, position);
            }
        }
    }
}