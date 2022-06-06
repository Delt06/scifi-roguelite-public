using DELTation.LeoEcsExtensions.Utilities;
using Health.Death;
using Leopotam.EcsLite;

namespace Movement.Ragdoll
{
    public class DeathRagdollSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public DeathRagdollSystem(EcsWorld world) =>
            _filter = world.Filter<RagdollDataView>()
                .Inc<DeathCommand>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                _filter.Read<RagdollDataView>(i)
                    .Ragdoll.Toggle(true);
            }
        }
    }
}