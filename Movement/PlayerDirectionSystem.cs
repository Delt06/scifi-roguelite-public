using _Shared;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class PlayerDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly SceneData _sceneData;

        public PlayerDirectionSystem(EcsWorld world, SceneData sceneData)
        {
            _filter = world.Filter<MovementData>()
                .Inc<PlayerTag>()
                .End();
            _sceneData = sceneData;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var cameraTransform = _sceneData.Camera.transform;
                var forward = Flatten(cameraTransform.forward);
                var right = Flatten(cameraTransform.right);
                var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                input = Vector2.ClampMagnitude(input, 1f);
                var direction = right * input.x + forward * input.y;

                ref var data = ref _filter.Get<MovementData>(i);
                data.Direction = direction;
            }
        }

        private static Vector3 Flatten(Vector3 vector)
        {
            vector.y = 0f;
            return vector.normalized;
        }
    }
}