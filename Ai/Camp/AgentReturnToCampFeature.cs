using DELTation.LeoEcsExtensions.Composition;
using DELTation.LeoEcsExtensions.Composition.Di;

namespace Ai.Camp
{
    public class AgentReturnToCampFeature : PrebuiltFeature
    {
        protected override void ConfigureBuilder(EcsFeatureBuilder featureBuilder)
        {
            featureBuilder
                .CreateAndAdd<AgentReturnToCampWhenDestinationIsTooFarSystem>()
                .CreateAndAdd<AgentReturnToCampWhenNoDestinationSystem>()
                .CreateAndAdd<AgentReturnToCampCommandSystem>()
                .OneFrame<ReturnToCampCommand>()
                ;
        }
    }
}