using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Attack
{
    public class StopDuringAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public StopDuringAttackSystem(EcsWorld world) =>
            _filter = world.Filter<MovementData>()
                .Inc<AttackState>()
                .Inc<StopDuringAttackTag>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                _filter.Get<MovementData>(i)
                    .Direction = Vector3.zero;
            }
        }
    }
}