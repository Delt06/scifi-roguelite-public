using _Shared.Utils;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Attack
{
    public class AttackStartSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public AttackStartSystem(EcsWorld world) =>
            _filter = world.Filter<AttackData>()
                .Inc<AttackStartCommand>()
                .Exc<ActiveAttackCooldown>()
                .Inc<IdleState>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var attackData = _filter.Read<AttackData>(i);
                ref var activeAttackData = ref _filter.Replace<IdleState, AttackState>(i);
                activeAttackData.Duration = attackData.Duration;
            }
        }
    }
}