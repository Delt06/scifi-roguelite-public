using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class CharacterControllerGravitySystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public CharacterControllerGravitySystem(EcsWorld world) =>
            _filter = world.Filter<CharacterControllerData>()
                .End();

        public void Run(EcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            foreach (var i in _filter)
            {
                ref var characterControllerData = ref _filter.Get<CharacterControllerData>(i);
                ref var gravityVelocity = ref characterControllerData.GravityVelocity;
                gravityVelocity += Physics.gravity * deltaTime;

                var characterController = characterControllerData.CharacterController;
                characterController.Move(characterControllerData.GravityVelocity * deltaTime);

                if (characterController.isGrounded)
                    gravityVelocity = Vector3.zero;
            }
        }
    }
}