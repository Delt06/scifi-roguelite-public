using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Ai
{
    public class AgentSetDestinationSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public AgentSetDestinationSystem(EcsWorld world) =>
            _filter = world.Filter<NavMeshAgentData>()
                .IncTransform()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var navMeshAgentData = _filter.Read<NavMeshAgentData>(i);
                var agent = navMeshAgentData.Agent;

                if (!_filter.Has<NavMeshDestinationData>(i))
                {
                    agent.ResetPath();
                    continue;
                }

                var destination = _filter.Read<NavMeshDestinationData>(i).Destination;

                var agentPosition = _filter.GetTransform(i).position;
                var sqrDistanceToDestination = (destination - agentPosition).sqrMagnitude;
                var sqrStoppingThreshold = navMeshAgentData.StoppingThreshold * navMeshAgentData.StoppingThreshold;
                if (sqrDistanceToDestination < sqrStoppingThreshold)
                    agent.ResetPath();
                else
                    agent.SetDestination(destination);
            }
        }
    }
}