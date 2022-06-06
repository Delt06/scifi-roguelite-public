using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Health
{
    public class HealthBarSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;
        private readonly EcsReadOnlyPool<HealthBarData> _healthBarDatas;
        private readonly EcsReadOnlyPool<HealthData> _healthDatas;

        public HealthBarSystem(EcsWorld world)
        {
            _filter = world.FilterAndIncUpdateOf<HealthData>()
                .Inc<HealthBarData>()
                .End();
            _healthDatas = world.GetPool<HealthData>().AsReadOnly();
            _healthBarDatas = world.GetPool<HealthBarData>().AsReadOnly();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var e in _filter)
            {
                var text = _healthBarDatas.Read(e).Text;
                var healthData = _healthDatas.Read(e);
                text.SetText("{0:0}/{1:0}", healthData.Health, healthData.MaxHealth);
            }
        }
    }
}