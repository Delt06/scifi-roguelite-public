using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ai
{
    public class AgentSnapSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly EcsReadOnlyPool<NavMeshAgentData> _agentData;
        private readonly EcsReadOnlyPool<UnityObjectData<Transform>> _transforms;

        public AgentSnapSystem(EcsWorld world)
        {
            _filter = world.Filter<NavMeshAgentData>()
                .IncTransform()
                .End();
            _agentData = world.GetReadOnlyPool<NavMeshAgentData>();
            _transforms = world.GetTransformPool().AsReadOnly();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var agent = _agentData.Read(i).Agent;
                var agentPosition = agent.nextPosition;
                var actualPosition = _transforms.Read(i).Object.position;
                var sqrDifference = (agentPosition - actualPosition).sqrMagnitude;
                const float maxDifference = 0.25f;
                if (sqrDifference > maxDifference * maxDifference)
                    agent.nextPosition = actualPosition;
            }
        }
    }
}