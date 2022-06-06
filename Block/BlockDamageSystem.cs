using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Health.Damage;
using Leopotam.EcsLite;

namespace Block
{
    public class BlockDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter _blocking;
        private readonly EcsFilter _dealDamageCommands;
        private readonly EcsWorld _world;

        public BlockDamageSystem(EcsWorld world)
        {
            _world = world;
            _blocking = world.Filter<BlockActiveTag>().End();
            _dealDamageCommands = world.Filter<DealDamageCommand>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var iCommand in _dealDamageCommands)
            {
                ref readonly var dealDamageCommand = ref _dealDamageCommands.Read<DealDamageCommand>(iCommand);
                if (!_blocking.Contains(dealDamageCommand.Target)) continue;

                ref var blockEvent = ref _world.NewPackedEntityWithWorld().Add<BlockEvent>();
                blockEvent.Blocker = dealDamageCommand.Target;
                blockEvent.Attacker = dealDamageCommand.Attacker;
                blockEvent.Position = dealDamageCommand.Position;
                blockEvent.DamageType = dealDamageCommand.Type;

                _dealDamageCommands.Del<DealDamageCommand>(iCommand);
            }
        }
    }
}