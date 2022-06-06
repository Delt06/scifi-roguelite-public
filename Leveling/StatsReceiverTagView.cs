using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;

namespace Leveling
{
    public class StatsReceiverTagView : ComponentView<StatsReceiverTag>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            entity.GetOrAdd<StatsUpdateRequest>();
        }
    }
}