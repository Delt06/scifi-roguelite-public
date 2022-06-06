using Block;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ai
{
    public class AgentBlockInputUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public AgentBlockInputUpdateSystem(EcsWorld world) =>
            _filter = world.Filter<AgentBlockData>()
                .Inc<BlockInputTag>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var agentBlockData = ref _filter.Get<AgentBlockData>(i);
                agentBlockData.RemainingTime -= Time.deltaTime;
                if (agentBlockData.RemainingTime > 0f) continue;

                _filter.Del<BlockInputTag>(i);
            }
        }
    }
}