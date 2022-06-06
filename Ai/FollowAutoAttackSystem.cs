using Attack;
using Block;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Ai
{
    public class FollowAutoAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public FollowAutoAttackSystem(EcsWorld world) =>
            _filter = world.Filter<FollowData>()
                .IncTransform()
                .Inc<FollowAutoAttackData>()
                .Inc<AttackData>()
                .Exc<FollowTargetIsOccludedTag>()
                .Exc<AttackStartCommand>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var followData = _filter.Read<FollowData>(i);
                var targetOrNull = followData.Target;
                if (targetOrNull == null) continue;

                var target = targetOrNull.Value;
                if (!target.IsAlive()) continue;

                var followAutoAttackData = _filter.Read<FollowAutoAttackData>(i);
                if (target.Has<BlockState>() && followAutoAttackData.SkipWhenBlocking)
                    continue;

                var offset = target.GetTransform().position - _filter.GetTransform(i).position;
                offset.y = 0f;
                var distance = offset.magnitude;
                var maxDistance = followAutoAttackData.MaxDistance;
                if (distance > maxDistance) continue;

                _filter.Add<AttackStartCommand>(i);
            }
        }
    }
}