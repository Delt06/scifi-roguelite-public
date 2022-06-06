using _Shared;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Health
{
    public class LookAtCameraSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly SceneData _sceneData;

        public LookAtCameraSystem(EcsWorld world, SceneData sceneData)
        {
            _sceneData = sceneData;
            _filter = world.Filter<LookAtCameraData>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var transform = _filter.Read<LookAtCameraData>(i).Transform;
                transform.forward = _sceneData.Camera.transform.forward;
            }
        }
    }
}