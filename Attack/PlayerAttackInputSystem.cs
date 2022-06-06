using _Shared;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Attack
{
    public class PlayerAttackInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public PlayerAttackInputSystem(EcsWorld world) =>
            _filter = world.Filter<AttackData>()
                .Inc<PlayerTag>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                if (Input.GetMouseButton(0))
                    _filter.GetOrAdd<AttackStartCommand>(i);
            }
        }
    }
}