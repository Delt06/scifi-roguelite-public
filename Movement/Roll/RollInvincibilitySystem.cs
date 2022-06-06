using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Health.Damage;
using Leopotam.EcsLite;

namespace Movement.Roll
{
    public class RollInvincibilitySystem : IEcsRunSystem
    {
        private readonly EcsFilter _dealDamageCommands;
        private readonly EcsFilter _rolling;

        public RollInvincibilitySystem(EcsWorld world)
        {
            _rolling = world.Filter<RollState>().End();
            _dealDamageCommands = world.Filter<DealDamageCommand>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var iCommand in _dealDamageCommands)
            {
                ref readonly var dealDamageCommand = ref _dealDamageCommands.Read<DealDamageCommand>(iCommand);
                if (!_rolling.Contains(dealDamageCommand.Target)) continue;

                var rollState = dealDamageCommand.Target.Read<RollState>();
                if (rollState.RemainingInvincibilityTime > 0f)
                {
                    dealDamageCommand.Target.GetOrAdd<DodgeEvent>().Position = dealDamageCommand.Position;
                    _dealDamageCommands.Del<DealDamageCommand>(iCommand);
                }
            }
        }
    }
}