using System;
using System.Collections.Generic;
using _Shared.Utils;
using Attack;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views;
using Health.Teams;
using Leopotam.EcsLite;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Health.Damage
{
    public class RangedAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly IComparer<RaycastHit> _hitDistanceComparer;
        private readonly RaycastHit[] _hits = new RaycastHit[20];
        private readonly HealthStaticData _staticData;
        private readonly EcsWorld _world;

        public RangedAttackSystem(EcsWorld world, HealthStaticData staticData)
        {
            _world = world;
            _staticData = staticData;
            _filter = world.Filter<RangedDamageData>()
                .Inc<AttackActivationEvent>()
                .Inc<TeamData>()
                .Inc<DamageData>()
                .End();
            _hitDistanceComparer = new DistanceComparer();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref readonly var rangedDamageData = ref _filter.Read<RangedDamageData>(i);
                ref readonly var damageData = ref _filter.Read<DamageData>(i);

                var shootFrom = rangedDamageData.ShootFrom;
                var origin = shootFrom.position;
                var direction = shootFrom.forward;

                var spreadAxis1 = shootFrom.up;
                var spreadAxis2 = Vector3.Cross(direction, spreadAxis1);
                var spreadAngles = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                spreadAngles *= rangedDamageData.MaxSpreadAngle;

                direction = Quaternion.AngleAxis(spreadAngles.x, spreadAxis1) *
                            Quaternion.AngleAxis(spreadAngles.y, spreadAxis2) *
                            direction;

                Debug.DrawRay(origin, direction, Color.red, 0.1f);

                var hitsCount = Physics.SphereCastNonAlloc(origin, rangedDamageData.Radius, direction, _hits,
                    rangedDamageData.MaxDistance,
                    _staticData.RangedAttackLayerMask,
                    QueryTriggerInteraction.Collide
                );

                var team = _filter.Read<TeamData>(i).Team;

                SortHitsByDistance(hitsCount);

                var effectiveHit = false;

                for (var hitIndex = 0; hitIndex < hitsCount; hitIndex++)
                {
                    var hit = _hits[hitIndex];
                    TryTriggerDealingDamage(i, hit.collider, damageData, rangedDamageData, direction, team,
                        out effectiveHit
                    );
                    if (!effectiveHit) continue;

                    PlayTrailEffect(rangedDamageData, hit.distance);
                    break;
                }

                if (!effectiveHit)
                    PlayTrailEffect(rangedDamageData, rangedDamageData.MaxDistance);
            }
        }

        private void TryTriggerDealingDamage(int i, Collider collider, DamageData damageData,
            in RangedDamageData rangedDamageData,
            Vector3 direction,
            Team team, out bool effectiveHit)
        {
            if (!collider.TryGetComponent(out IEntityView entityView))
            {
                effectiveHit = true;
                return;
            }

            if (!entityView.TryGetEntity(out var entity))
            {
                effectiveHit = true;
                return;
            }

            if (!entity.Has<TeamData>())
            {
                effectiveHit = true;
                return;
            }

            if (entity.Read<TeamData>().Team == team)
            {
                effectiveHit = false;
                return;
            }

            var commandEntity = _world.NewPackedEntityWithWorld();
            ref var dealDamageCommand = ref commandEntity.Add<DealDamageCommand>();
            dealDamageCommand.Target = entity;
            dealDamageCommand.Damage = damageData.Damage;
            dealDamageCommand.AttackerTeam = team;
            dealDamageCommand.Impulse = direction * rangedDamageData.Impulse;
            dealDamageCommand.Type = DamageType.Ranged;
            dealDamageCommand.Attacker = _world.PackEntityWithWorld(i);
            dealDamageCommand.Position = collider.GetCenter();
            effectiveHit = true;
        }

        private static void PlayTrailEffect(in RangedDamageData rangedDamageData, float hitDistance)
        {
            var trail = rangedDamageData.Trail;
            var mainModule = trail.main;
            mainModule.startSpeed = hitDistance / mainModule.startLifetime.constant;
            trail.Play();
        }

        private void SortHitsByDistance(int hitsCount)
        {
            Array.Sort(_hits, 0, hitsCount, _hitDistanceComparer);
        }

        private class DistanceComparer : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance);
        }
    }
}