using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Ai
{
    public class FollowRotationOverrideSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly MovementStaticData _staticData;

        public FollowRotationOverrideSystem(EcsWorld world, MovementStaticData staticData)
        {
            _staticData = staticData;
            _filter = world.Filter<FollowData>()
                .Exc<LookRotationOverrideData>()
                .Exc<FollowTargetIsOccludedTag>()
                .IncTransform()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref readonly var followData = ref _filter.Read<FollowData>(i);
                var targetOrNull = followData.Target;
                if (targetOrNull == null) continue;

                var target = targetOrNull.Value;
                if (!target.IsAlive()) continue;

                var targetPosition = target.GetTransform().position;
                var position = _filter.GetTransform(i).position;
                var offset = targetPosition - position;
                var distance = offset.magnitude;
                if (distance > followData.MaxDistanceToLook) continue;

                var direction = Vector3.ClampMagnitude(offset, 1f);
                direction.y = 0f;
                var threshold = _staticData.RotationDirectionThreshold;
                if (direction.sqrMagnitude < threshold * threshold) continue;

                _filter.Add<LookRotationOverrideData>(i).Rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }
    }
}