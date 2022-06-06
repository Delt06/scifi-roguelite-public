using Attack;
using Block;
using DELTation.LeoEcsExtensions.Utilities;
using Health.Damage;
using Leopotam.EcsLite;

namespace Stun
{
    public class BlockStunSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public BlockStunSystem(EcsWorld world) =>
            _filter = world.Filter<BlockEvent>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref readonly var blockEvent = ref _filter.Read<BlockEvent>(i);
                if (blockEvent.DamageType == DamageType.Ranged) continue;
                if (!blockEvent.Blocker.Unpack(out _, out var blockerIdx)) continue;
                if (!blockEvent.Attacker.Unpack(out _, out var attackerIdx)) continue;

                if (!_filter.Has<BlockData>(blockerIdx)) continue;
                if (!_filter.Has<AttackState>(attackerIdx)) continue;

                ref var stunState = ref AttackUtils.StopAttackAndTransitionTo<StunState>(_filter, attackerIdx);
                stunState.Duration = _filter.Read<BlockData>(blockerIdx).StunDuration;
                stunState.ElapsedTime = 0f;
            }
        }
    }
}