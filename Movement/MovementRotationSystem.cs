using _Shared.Utils;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class MovementRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly MovementStaticData _staticData;

        public MovementRotationSystem(EcsWorld world, MovementStaticData staticData)
        {
            _staticData = staticData;
            _filter = world.Filter<MovementData>()
                .IncTransform()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var data = ref _filter.Get<MovementData>(i);

                Quaternion targetRotation;
                if (_filter.Has<LookRotationOverrideData>(i))
                {
                    targetRotation = _filter.Read<LookRotationOverrideData>(i).Rotation;
                }
                else
                {
                    var direction = data.Direction;
                    direction.y = 0f;

                    var threshold = _staticData.RotationDirectionThreshold;
                    if (direction.sqrMagnitude < threshold * threshold) continue;
                    targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                }

                var transform = _filter.GetTransform(i);
                transform.rotation =
                    QuaternionUtil.SmoothDamp(transform.rotation, targetRotation,
                        ref data.RotationAngularVelocity,
                        data.RotationDampTime, Time.deltaTime
                    );
            }
        }
    }
}