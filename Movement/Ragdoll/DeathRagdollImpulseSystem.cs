using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Health.Damage;
using Health.Death;
using Leopotam.EcsLite;

namespace Movement.Ragdoll
{
    public class DeathRagdollImpulseSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public DeathRagdollImpulseSystem(EcsWorld world) =>
            _filter = world.Filter<RagdollDataView>()
                .Inc<DeathCommand>()
                .Inc<DamageImpulseData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var ragdoll = _filter.Read<RagdollDataView>(i).Ragdoll;
                var impulse = _filter.Read<DamageImpulseData>(i).Impulse;
                ragdoll.AddImpulse(impulse);
            }
        }
    }
}