using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using Movement.Ragdoll;
using UnityEngine;

namespace Health.Death
{
    public class DeathAnimationSystem : IEcsRunSystem
    {
        private static readonly int IsDeadId = Animator.StringToHash("IsDead");
        private readonly EcsFilter _filter;

        public DeathAnimationSystem(EcsWorld world) =>
            _filter = world.Filter<DeathCommand>()
                .Inc<AnimatorData>()
                .Exc<RagdollDataView>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var animator = _filter.Get<AnimatorData>(i).Animator;
                animator.SetBool(IsDeadId, true);
            }
        }
    }
}