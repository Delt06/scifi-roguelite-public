using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;

namespace Health
{
    public class HealthDataView : ComponentView<HealthData>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            ref var healthData = ref entity.Modify<HealthData>();
            healthData.Health = healthData.MaxHealth;
        }
    }
}