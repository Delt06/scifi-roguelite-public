using _Shared;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Ai
{
    public class PlayerFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly EcsReadWritePool<FollowData> _followDatas;
        private readonly SceneData _sceneData;

        public PlayerFollowSystem(EcsWorld world, SceneData sceneData)
        {
            _sceneData = sceneData;
            _filter = world.Filter<FollowData>()
                .Inc<FollowPlayerTag>()
                .End();
            _followDatas = world.GetPool<FollowData>().AsReadWrite();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var followData = ref _followDatas.Get(i);
                var player = _sceneData.Player;
                followData.Target =
                    player != null && player.TryGetEntity(out var playerEntity) ? playerEntity : default;
            }
        }
    }
}