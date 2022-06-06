using Camps;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Ai.Camp
{
    public class AgentReturnToCampCommandSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public AgentReturnToCampCommandSystem(EcsWorld world) =>
            _filter = world.Filter<NavMeshAgentData>()
                .Inc<CampRef>()
                .Inc<ReturnToCampCommand>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var navMeshDestinationData = ref _filter.GetOrAdd<NavMeshDestinationData>(i);
                navMeshDestinationData.Destination = _filter.Read<CampRef>(i).SpawnPosition;
            }
        }
    }
}