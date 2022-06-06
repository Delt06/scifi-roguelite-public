using _Shared;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Block
{
    public class PlayerBlockInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public PlayerBlockInputSystem(EcsWorld world) =>
            _filter = world.Filter<PlayerTag>()
                .Inc<BlockData>()
                .End();

        public void Run(EcsSystems systems)
        {
            var input = Input.GetKey(KeyCode.F);

            foreach (var i in _filter)
            {
                if (input)
                    _filter.GetOrAdd<BlockInputTag>(i);
                else
                    _filter.Del<BlockInputTag>(i);
            }
        }
    }
}