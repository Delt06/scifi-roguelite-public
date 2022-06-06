using Camps;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Spawning
{
    public class CampSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter _campsFilter;
        private readonly IEnemyFactory _enemyFactory;
        private readonly EcsFilter _gameRestartFilter;

        public CampSpawnSystem(EcsWorld world, IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _campsFilter = world.Filter<CampData>()
                .IncTransform()
                .End();
            _gameRestartFilter = world.Filter<GameRestartCommand>().End();
        }

        public void Init(EcsSystems systems)
        {
            Spawn();
        }

        public void Run(EcsSystems systems)
        {
            if (EcsFilterExtensions.IsEmpty(_gameRestartFilter)) return;

            Spawn();
        }

        private void Spawn()
        {
            foreach (var i in _campsFilter)
            {
                ref readonly var campData = ref _campsFilter.Read<CampData>(i);
                var campPosition = _campsFilter.GetTransform(i).position;
                var spawnPoints = campData.SpawnPoints;

                foreach (var spawnPoint in spawnPoints)
                {
                    var position = spawnPoint.Position;
                    var entity = _enemyFactory.Spawn(spawnPoint.EnemyType, position);
                    ref var campRef = ref entity.Add<CampRef>();
                    campRef.CampPosition = campPosition;
                    campRef.SpawnPosition = position;
                }
            }
        }
    }
}