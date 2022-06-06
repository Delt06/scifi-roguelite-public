using _Shared.Utils;
using Attack;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Stun
{
    public class StunUpdateSystem : IEcsRunSystem
    {
        private static readonly int StunId = Animator.StringToHash("Stun");
        private static readonly int StunProgressId = Animator.StringToHash("StunProgress");
        private readonly EcsFilter _filter;

        public StunUpdateSystem(EcsWorld world) =>
            _filter = world.Filter<StunState>()
                .Inc<AnimatorData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var stunState = ref _filter.Get<StunState>(i);
                stunState.ElapsedTime += Time.deltaTime;

                var animator = _filter.Read<AnimatorData>(i).Animator;
                var progress = stunState.ElapsedTime / stunState.Duration;
                if (progress >= 1f)
                {
                    animator.SetBool(StunId, false);
                    _filter.Replace<StunState, IdleState>(i);
                }
                else
                {
                    animator.SetBool(StunId, true);
                    animator.SetFloat(StunProgressId, progress);
                }
            }
        }
    }
}