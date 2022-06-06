using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Attack
{
    public class AttackAnimationSystem : IEcsRunSystem
    {
        private static readonly int IsAttackingId = Animator.StringToHash("IsAttacking");
        private static readonly int AttackProgressId = Animator.StringToHash("AttackProgress");
        private readonly AttackStaticData _attackStaticData;
        private readonly EcsFilter _filter;

        public AttackAnimationSystem(EcsWorld world, AttackStaticData attackStaticData)
        {
            _attackStaticData = attackStaticData;
            _filter = world.Filter<AttackData>()
                .Inc<AnimatorData>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var animator = _filter.Read<AnimatorData>(i).Animator;
                bool isAttacking;
                float weight;

                if (_filter.Has<AttackState>(i))
                {
                    var activeAttackData = _filter.Read<AttackState>(i);
                    var progress = activeAttackData.GetNormalizedTime();
                    animator.SetFloat(AttackProgressId, progress);
                    isAttacking = true;
                    weight = _attackStaticData.AttackLayerWeightOverProgress.Evaluate(progress);
                }
                else
                {
                    isAttacking = false;
                    weight = 0f;
                }

                animator.SetBool(IsAttackingId, isAttacking);
                animator.SetLayerWeight(_attackStaticData.AttackLayerIndex, weight);
            }
        }
    }
}