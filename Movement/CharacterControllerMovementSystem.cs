using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class CharacterControllerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public CharacterControllerMovementSystem(EcsWorld world)
        {
            _filter = world.Filter<MovementData>()
                .Inc<CharacterControllerData>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref readonly var data = ref _filter.Read<MovementData>(i);
                var direction = data.Direction;
                var velocity = direction * data.MovementSpeed;
                var motion = velocity * Time.deltaTime + data.RootMotion;
                
                var characterController = _filter.Read<CharacterControllerData>(i).CharacterController;
                characterController.Move(motion);
            }
        }
    }
}