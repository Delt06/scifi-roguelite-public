using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class SlopeDirectionCorrectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly MovementStaticData _staticData;

        public SlopeDirectionCorrectionSystem(EcsWorld world, MovementStaticData staticData)
        {
            _staticData = staticData;
            _filter = world.Filter<MovementData>()
                .Inc<CharacterControllerData>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var data = ref _filter.Get<MovementData>(i);
                var direction = data.Direction;

                var characterController = _filter.Read<CharacterControllerData>(i)
                    .CharacterController;

                if (Physics.Raycast(new Ray(characterController.transform.position + Vector3.up * 0.5f, Vector3.down),
                        out var hit, 1f, _staticData.EnvironmentLayerMask
                    ))
                {
                    var normal = hit.normal;
                    var slopeLimitCos = Mathf.Cos(characterController.slopeLimit * Mathf.Deg2Rad);

                    if (Vector3.Dot(Vector3.up, normal) >= slopeLimitCos)
                    {
                        var rotation = Quaternion.LookRotation(Vector3.forward, normal);
                        direction = rotation * direction;
                    }
                }

                data.Direction = direction;
            }
        }
    }
}