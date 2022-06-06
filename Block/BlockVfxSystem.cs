using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Vfx;

namespace Block
{
    public class BlockVfxSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly IVfxFactory _vfxFactory;

        public BlockVfxSystem(EcsWorld world, IVfxFactory vfxFactory)
        {
            _vfxFactory = vfxFactory;
            _filter = world.Filter<BlockEvent>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var position = _filter.Read<BlockEvent>(i).Position;
                _vfxFactory.Create(sd => sd.BlockFxPrefab, position);
            }
        }
    }
}