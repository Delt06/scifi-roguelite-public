using Attack;
using Block;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement.Roll;
using Stun;
using UnityEngine;

namespace Movement
{
    public class ExitIdleAnimationSystem : IEcsRunSystem
    {
        private static readonly int ExitIdleId = Animator.StringToHash("ExitIdle");
        private readonly EcsFilter _attackFilter;
        private readonly EcsFilter _blockFilter;
        private readonly EcsFilter _idleFilter;
        private readonly EcsFilter _rollFilter;
        private readonly EcsFilter _stunState;

        public ExitIdleAnimationSystem(EcsWorld world)
        {
            _rollFilter = GetAnimationFilter(world).Inc<RollState>().End();
            _idleFilter = GetAnimationFilter(world).Inc<IdleState>().End();
            _attackFilter = GetAnimationFilter(world).Inc<AttackState>().End();
            _blockFilter = GetAnimationFilter(world).Inc<BlockState>().End();
            _stunState = GetAnimationFilter(world).Inc<StunState>().End();
        }

        public void Run(EcsSystems systems)
        {
            // true
            MapStateToExitIdle(_rollFilter, true);
            MapStateToExitIdle(_blockFilter, true);
            MapStateToExitIdle(_stunState, true);

            // false
            MapStateToExitIdle(_idleFilter, false);
            MapStateToExitIdle(_attackFilter, false);
        }

        private static EcsWorld.Mask GetAnimationFilter(EcsWorld world) =>
            world.Filter<AnimatorData>();

        private static void MapStateToExitIdle(EcsFilter filter, bool exitIdleValue)
        {
            foreach (var i in filter)
            {
                var animator = filter.Get<AnimatorData>(i).Animator;
                animator.SetBool(ExitIdleId, exitIdleValue);
            }
        }
    }
}