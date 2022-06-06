using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;

namespace Ai
{
    public class NavMeshAgentDataView : ComponentView<NavMeshAgentData>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            var agent = entity.Read<NavMeshAgentData>().Agent;
            agent.updatePosition = agent.updateRotation = agent.updateUpAxis = false;
        }
    }
}