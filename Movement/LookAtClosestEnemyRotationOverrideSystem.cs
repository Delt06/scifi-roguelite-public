using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views;
using Health;
using Health.Teams;
using Leopotam.EcsLite;
using Movement.Roll;
using UnityEngine;

namespace Movement
{
    public class LookAtClosestEnemyRotationOverrideSystem : IEcsRunSystem
    {
        private readonly Collider[] _buffer = new Collider[20];
        private readonly EcsFilter _enemyFilter;
        private readonly EcsFilter _filter;
        private readonly HealthStaticData _healthStaticData;
        private readonly EcsReadOnlyPool<LookAtClosestEnemyData> _lookAtClosestEnemyData;
        private readonly EcsReadWritePool<LookRotationOverrideData> _lookRotationOverrideData;
        private readonly EcsReadOnlyPool<TeamData> _teamData;
        private readonly EcsReadOnlyPool<UnityObjectData<Transform>> _transforms;

        public LookAtClosestEnemyRotationOverrideSystem(EcsWorld world, HealthStaticData healthStaticData)
        {
            _healthStaticData = healthStaticData;
            _filter = world.Filter<LookAtClosestEnemyData>()
                .Exc<LookRotationOverrideData>()
                .Exc<RollState>()
                .IncTransform()
                .Inc<TeamData>()
                .End();

            _enemyFilter = world.Filter<TeamData>()
                .IncTransform()
                .End();

            _transforms = world.GetTransformPool().AsReadOnly();
            _lookAtClosestEnemyData = world.GetReadOnlyPool<LookAtClosestEnemyData>();
            _teamData = world.GetReadOnlyPool<TeamData>();
            _lookRotationOverrideData = world.GetReadWritePool<LookRotationOverrideData>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var idx in _filter)
            {
                var transform = _transforms.Read(idx).Object;
                var data = _lookAtClosestEnemyData.Read(idx);
                var team = _teamData.Read(idx).Team;

                var center = transform.TransformPoint(data.CheckCenterOffset);
                var count = Physics.OverlapSphereNonAlloc(center, data.CheckRadius, _buffer,
                    _healthStaticData.CharactersLayerMask
                );

                var origin = transform.position;
                var minSqrDistance = float.PositiveInfinity;
                Vector3? minOffset = null;

                for (var i = 0; i < count; i++)
                {
                    var collider = _buffer[i];
                    if (!collider.TryGetComponent(out IEntityView entityView)) continue;
                    if (!entityView.TryGetEntity(out var entity)) continue;
                    if (!_enemyFilter.Contains(entity)) continue;

                    var otherTeam = entity.Read<TeamData>().Team;
                    if (otherTeam == team) continue;

                    var offset = entity.GetTransform().position - origin;
                    offset.y = 0f;
                    var sqrDistance = offset.sqrMagnitude;
                    if (sqrDistance >= minSqrDistance) continue;

                    minSqrDistance = sqrDistance;
                    minOffset = offset;
                }

                if (minOffset == null) continue;

                ref var lookRotationOverrideData = ref _lookRotationOverrideData.Add(idx);
                lookRotationOverrideData.Rotation = Quaternion.LookRotation(minOffset.Value, Vector3.up);
            }
        }
    }
}