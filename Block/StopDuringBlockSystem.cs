using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Block
{
    public class StopDuringBlockSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public StopDuringBlockSystem(EcsWorld world) =>
            _filter = world.Filter<BlockState>()
                .Inc<MovementData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                _filter.Get<MovementData>(i).Direction = Vector3.zero;
            }
        }
    }
}