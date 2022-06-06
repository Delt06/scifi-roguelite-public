using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class MovementAnimationSystem : IEcsRunSystem
    {
        private static readonly int IsMovingId = Animator.StringToHash("IsMoving");
        private static readonly int MovementSpeedId = Animator.StringToHash("MovementSpeed");
        private readonly EcsFilter _filter;
        private readonly MovementStaticData _staticData;

        public MovementAnimationSystem(EcsWorld world, MovementStaticData staticData)
        {
            _filter = world.Filter<AnimatorData>()
                .Inc<MovementData>()
                .End();
            _staticData = staticData;
        }

        public void Run(EcsSystems systems)
        {
            var movementThreshold = _staticData.MovementThreshold;

            foreach (var i in _filter)
            {
                var animator = _filter.Read<AnimatorData>(i).Animator;
                var data = _filter.Read<MovementData>(i);
                var directionMagnitude = data.Direction.magnitude;

                animator.SetBool(IsMovingId, directionMagnitude > movementThreshold);
                animator.SetFloat(MovementSpeedId, directionMagnitude *
                                                   data.MovementSpeed *
                                                   data.AnimationSpeedMultiplier
                );
            }
        }
    }
}