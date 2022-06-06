using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Health.Death
{
    public class DeathCommandSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public DeathCommandSystem(EcsWorld world) =>
            _filter = world.FilterAndIncUpdateOf<HealthData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                if (_filter.Read<HealthData>(i).Health <= 0f)
                    _filter.Add<DeathCommand>(i);
            }
        }
    }
}