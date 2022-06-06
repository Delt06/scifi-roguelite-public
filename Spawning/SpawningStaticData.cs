using DELTation.LeoEcsExtensions.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Spawning
{
    [CreateAssetMenu]
    public class SpawningStaticData : ScriptableObject
    {
        [SerializeField] [Required] private EntityView _playerPrefab;
        [SerializeField] [Min(0f)] private float _gameRestartAfterPlayerDeathDelay = 2f;

        public EntityView PlayerPrefab => _playerPrefab;

        public float GameRestartAfterPlayerDeathDelay => _gameRestartAfterPlayerDeathDelay;
    }
}