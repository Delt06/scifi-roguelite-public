using Attack;
using Block;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ai
{
    public class AgentBlockInputStartSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly EcsFilter _targetFilter;

        public AgentBlockInputStartSystem(EcsWorld world)
        {
            _filter = world.Filter<IdleState>()
                .Inc<FollowData>()
                .Inc<AgentBlockData>()
                .Inc<BlockData>()
                .IncTransform()
                .End();
            _targetFilter = world.Filter<AttackState>()
                .IncTransform()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var targetOrDefault = _filter.Read<FollowData>(i).Target;
                if (targetOrDefault == null) continue;

                var target = targetOrDefault.Value;
                if (!_targetFilter.Contains(target)) continue;

                ref var agentBlockData = ref _filter.Get<AgentBlockData>(i);
                var distance = Vector3.Distance(
                    _filter.GetTransform(i).position,
                    target.GetTransform().position
                );
                if (distance > agentBlockData.MaxDistance) continue;

                _filter.GetOrAdd<BlockInputTag>(i);
                agentBlockData.RemainingTime = agentBlockData.Duration;
            }
        }
    }
}