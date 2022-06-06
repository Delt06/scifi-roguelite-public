using _Shared;
using Leopotam.EcsLite;

namespace Maps
{
    public class MapGatesOpenOnStartSystem : IEcsInitSystem
    {
        private readonly IGateService _gateService;
        private readonly SceneData _sceneData;

        public MapGatesOpenOnStartSystem(SceneData sceneData, IGateService gateService)
        {
            _sceneData = sceneData;
            _gateService = gateService;
        }

        public void Init(EcsSystems systems)
        {
            var mapData = _sceneData.MapData;
            var gates = mapData.Gates;

            foreach (var gateData in gates)
            {
                _gateService.Refresh(gateData.Gate, gateData.Guid);
            }
        }
    }
}