using System.Collections.Generic;
using _Shared.Utils;
using Attack;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views;
using Health.Teams;
using Leopotam.EcsLite;
using UnityEngine;

namespace Health.Damage
{
    public class MeleeAttackSystem : IEcsRunSystem
    {
        private readonly HashSet<IEntityView> _alreadyProcessed = new HashSet<IEntityView>();
        private readonly EcsFilter _filter;
        private readonly Collider[] _overlapsBuffer = new Collider[20];
        private readonly HealthStaticData _staticData;
        private readonly EcsWorld _world;

        public MeleeAttackSystem(EcsWorld world, HealthStaticData staticData)
        {
            _world = world;
            _staticData = staticData;
            _filter = world.Filter<MeleeDamageData>()
                .Inc<AttackActivationEvent>()
                .Inc<TeamData>()
                .Inc<DamageData>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref readonly var meleeDamageData = ref _filter.Read<MeleeDamageData>(i);
                ref readonly var damageData = ref _filter.Read<DamageData>(i);

                var (center, size, orientation) = meleeDamageData.Weapon.GetHitBoxInfo();
                var overlapsCount = Physics.OverlapBoxNonAlloc(center, size * 0.5f, _overlapsBuffer, orientation,
                    _staticData.CharactersLayerMask,
                    QueryTriggerInteraction.Collide
                );

                var team = _filter.Read<TeamData>(i).Team;

                Vector3? anyPosition = null;

                for (var overlapIndex = 0; overlapIndex < overlapsCount; overlapIndex++)
                {
                    var collider = _overlapsBuffer[overlapIndex];
                    if (TryTriggerDealingDamage(i, collider, damageData, meleeDamageData, team, out var position))
                        anyPosition ??= position;

                    _overlapsBuffer[overlapIndex] = null;
                }

                _alreadyProcessed.Clear();

                if (anyPosition.HasValue)
                    meleeDamageData.Weapon.PlayFxAt(anyPosition.Value);
            }
        }

        private bool TryTriggerDealingDamage(int i, Collider collider, DamageData damageData,
            in MeleeDamageData meleeDamageData, Team team,
            out Vector3 position)
        {
            position = default;
            if (!collider.TryGetComponent(out IEntityView entityView)) return false;
            if (_alreadyProcessed.Contains(entityView)) return false;

            _alreadyProcessed.Add(entityView);

            if (!entityView.TryGetEntity(out var entity)) return false;

            var commandEntity = _world.NewPackedEntityWithWorld();
            ref var dealDamageCommand = ref commandEntity.Add<DealDamageCommand>();
            dealDamageCommand.Target = entity;
            dealDamageCommand.Damage = damageData.Damage;
            dealDamageCommand.AttackerTeam = team;
            dealDamageCommand.Impulse = meleeDamageData.Weapon.Forward * meleeDamageData.Impulse;
            dealDamageCommand.Type = DamageType.Melee;
            dealDamageCommand.Attacker = _world.PackEntityWithWorld(i);
            dealDamageCommand.Position = position = collider.GetCenter();

            return true;
        }
    }
}