using DELTation.LeoEcsExtensions.Utilities;
using Health.Teams;
using Leopotam.EcsLite;

namespace Health.Damage
{
    public class DealDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter _attackedFilter;
        private readonly EcsFilter _commandFilter;
        private readonly EcsReadWritePool<DamageImpulseData> _damageImpulseDatas;
        private readonly EcsReadOnlyPool<DealDamageCommand> _dealDamageCommands;
        private readonly EcsObservablePool<HealthData> _healthDatas;
        private readonly EcsReadOnlyPool<TeamData> _teamDatas;

        public DealDamageSystem(EcsWorld world)
        {
            _commandFilter = world.Filter<DealDamageCommand>()
                .End();
            _attackedFilter = world.Filter<HealthData>()
                .Inc<TeamData>()
                .End();
            _dealDamageCommands = world.GetPool<DealDamageCommand>().AsReadOnly();
            _teamDatas = world.GetPool<TeamData>().AsReadOnly();
            _healthDatas = world.GetPool<HealthData>().AsObservable();
            _damageImpulseDatas = world.GetPool<DamageImpulseData>().AsReadWrite();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var e in _commandFilter)
            {
                ref readonly var dealDamageCommand = ref _dealDamageCommands.Read(e);
                var target = dealDamageCommand.Target;
                if (!_attackedFilter.Contains(target)) continue;
                if (!target.Unpack(out _, out var iTarget)) continue;

                var teamData = _teamDatas.Read(iTarget);
                if (teamData.Team == dealDamageCommand.AttackerTeam) continue;

                ref var healthData = ref _healthDatas.Modify(iTarget);
                healthData.Health -= dealDamageCommand.Damage;

                _damageImpulseDatas.GetOrAdd(iTarget)
                    .Impulse += dealDamageCommand.Impulse;
            }
        }
    }
}