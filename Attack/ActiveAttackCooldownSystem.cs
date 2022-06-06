using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Attack
{
    public class ActiveAttackCooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public ActiveAttackCooldownSystem(EcsWorld world) =>
            _filter = world.Filter<ActiveAttackCooldown>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var activeAttackCooldown = ref _filter.Get<ActiveAttackCooldown>(i);
                activeAttackCooldown.RemainingTime -= Time.deltaTime;
                if (activeAttackCooldown.RemainingTime > 0f) continue;

                _filter.Del<ActiveAttackCooldown>(i);
            }
        }
    }
}