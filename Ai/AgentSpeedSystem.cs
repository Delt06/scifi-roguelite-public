using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;

namespace Ai
{
    public class AgentSpeedSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly EcsReadOnlyPool<MovementData> _movementData;
        private readonly EcsReadOnlyPool<NavMeshAgentData> _navMeshAgentData;

        public AgentSpeedSystem(EcsWorld world)
        {
            _filter = world.Filter<NavMeshAgentData>()
                .Inc<MovementData>()
                .End();
            _navMeshAgentData = world.GetReadOnlyPool<NavMeshAgentData>();
            _movementData = world.GetReadOnlyPool<MovementData>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var agent = _navMeshAgentData.Read(i).Agent;
                var movementSpeed = _movementData.Read(i).MovementSpeed;
                agent.speed = movementSpeed;
            }
        }
    }
}