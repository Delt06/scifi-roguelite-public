using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement.Roll
{
    public class RollInvincibilityTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public RollInvincibilityTimeSystem(EcsWorld world) =>
            _filter = world.Filter<RollState>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                _filter.Get<RollState>(i).RemainingInvincibilityTime -= Time.deltaTime;
            }
        }
    }
}