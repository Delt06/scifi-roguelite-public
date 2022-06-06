using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Health.Death
{
    public class DeathHealthBarSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public DeathHealthBarSystem(EcsWorld world) =>
            _filter = world.Filter<DeathCommand>()
                .Inc<HealthBarData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var root = _filter.Read<HealthBarData>(i).Root;
                Object.Destroy(root);
                _filter.Del<HealthBarData>(i);
            }
        }
    }
}