using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Ai
{
    public class AgentFollowDestinationSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly MovementStaticData _movementStaticData;

        public AgentFollowDestinationSystem(EcsWorld world, MovementStaticData movementStaticData)
        {
            _movementStaticData = movementStaticData;
            _filter = world.Filter<NavMeshAgentData>()
                .IncTransform()
                .Inc<FollowData>()
                .Exc<NavMeshDestinationData>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var followData = ref _filter.Get<FollowData>(i);

                var targetOrNull = followData.Target;
                if (targetOrNull == null) continue;

                var target = targetOrNull.Value;
                if (!target.IsAlive()) continue;

                var currentPosition = _filter.GetTransform(i).position;
                var targetPosition = target.GetTransform().position;
                var offset = targetPosition - currentPosition;
                var direction = offset.normalized;
                var distance = offset.magnitude;

                var desiredDistance = followData.DesiredDistance;

                Vector3 destination;
                if (distance > _movementStaticData.MovementThreshold &&
                    ViewToTargetIsOccluded(followData, direction, distance))
                {
                    destination = targetPosition;
                    _filter.GetOrAdd<FollowTargetIsOccludedTag>(i);
                }
                else
                {
                    var directionTowardsCurrentPosition = (currentPosition - targetPosition).normalized;
                    destination = targetPosition + desiredDistance * directionTowardsCurrentPosition;
                }

                _filter.Add<NavMeshDestinationData>(i).Destination = destination;
            }
        }

        private bool ViewToTargetIsOccluded(in FollowData followData, Vector3 direction, float distance)
        {
            var origin = followData.VisibilityCheckRayStart.position;
            var ray = new Ray(origin, direction);
            var layerMask = _movementStaticData.EnvironmentLayerMask;
            var radius = followData.VisibilityCheckRayRadius;
            return Physics.SphereCast(ray, radius, distance, layerMask);
        }
    }
}