using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Block
{
    public class BlockUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public BlockUpdateSystem(EcsWorld world) =>
            _filter = world.Filter<BlockState>()
                .Inc<BlockData>()
                .Exc<BlockActiveTag>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var blockState = ref _filter.Get<BlockState>(i);
                blockState.ElapsedTime += Time.deltaTime;

                var timeToActivate = _filter.Read<BlockData>(i).TimeToActivate;
                if (blockState.ElapsedTime >= timeToActivate) _filter.Add<BlockActiveTag>(i);
            }
        }
    }
}