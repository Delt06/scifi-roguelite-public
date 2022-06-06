using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Stun
{
    public class StopDuringStunSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public StopDuringStunSystem(EcsWorld world) =>
            _filter = world.Filter<StunState>()
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