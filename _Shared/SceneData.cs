using Cinemachine;
using DELTation.LeoEcsExtensions.Views;
using Maps;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Shared
{
    public class SceneData : MonoBehaviour
    {
        [Required] [SerializeField]
        private Camera _camera;

        [SerializeField] [Required] private Transform _playerSpawnPoint;

        [SerializeField] [Required] private CinemachineVirtualCamera _playerVirtualCamera;

        [SerializeField] [Required] private MapData _mapData;

        public Camera Camera => _camera;

        public Transform PlayerSpawnPoint => _playerSpawnPoint;

        public EntityView Player { get; set; }

        public CinemachineVirtualCamera PlayerVirtualCamera => _playerVirtualCamera;

        public MapData MapData => _mapData;
    }
}