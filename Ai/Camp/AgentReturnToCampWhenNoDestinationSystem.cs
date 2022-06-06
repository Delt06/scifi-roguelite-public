using Camps;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Ai.Camp
{
    public class AgentReturnToCampWhenNoDestinationSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public AgentReturnToCampWhenNoDestinationSystem(EcsWorld world) =>
            _filter = world.Filter<CampRef>()
                .Inc<NavMeshAgentData>()
                .Exc<NavMeshDestinationData>()
                .Exc<ReturnToCampCommand>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                _filter.Add<ReturnToCampCommand>(i);
            }
        }
    }
}