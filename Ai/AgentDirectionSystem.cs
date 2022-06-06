using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Ai
{
    public class AgentDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public AgentDirectionSystem(EcsWorld world) =>
            _filter = world.Filter<NavMeshAgentData>()
                .Inc<MovementData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var agent = _filter.Read<NavMeshAgentData>(i).Agent;
                var velocity = agent.velocity;
                var direction = agent.hasPath ? velocity / agent.speed : Vector3.zero;

                ref var movementData = ref _filter.Get<MovementData>(i);
                movementData.Direction = direction;
            }
        }
    }
}