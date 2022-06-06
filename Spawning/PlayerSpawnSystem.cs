using _Shared;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Spawning
{
    public class PlayerSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly SceneData _sceneData;
        private readonly SpawningStaticData _spawningStaticData;

        public PlayerSpawnSystem(SceneData sceneData, SpawningStaticData spawningStaticData, EcsWorld world)
        {
            _sceneData = sceneData;
            _spawningStaticData = spawningStaticData;
            _filter = world.Filter<GameRestartCommand>().End();
        }

        public void Init(EcsSystems systems)
        {
            Spawn();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()) return;
            Spawn();
        }

        private void Spawn()
        {
            if (_sceneData.Player != null)
                _sceneData.Player.Destroy();

            var prefab = _spawningStaticData.PlayerPrefab;
            var position = _sceneData.PlayerSpawnPoint.position;
            var rotation = _sceneData.PlayerSpawnPoint.rotation;
            var playerEntityView = Object.Instantiate(prefab, position, rotation);
            _sceneData.Player = playerEntityView;
            _sceneData.PlayerVirtualCamera.Follow = playerEntityView.transform;
            _sceneData.PlayerVirtualCamera.PreviousStateIsValid = false;
        }
    }
}