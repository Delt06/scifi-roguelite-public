using DELTation.LeoEcsExtensions.Composition;
using DELTation.LeoEcsExtensions.Composition.Di;
using Leveling.Energy;

namespace Leveling
{
    public class StatsFeature : PrebuiltFeature
    {
        protected override void ConfigureBuilder(EcsFeatureBuilder featureBuilder)
        {
            featureBuilder
                .CreateAndAdd<DamageStatsUpdateSystem>()
                .CreateAndAdd<HealthStatsUpdateSystem>()
                .OneFrame<StatsUpdateRequest>()
                ;

            featureBuilder
                .CreateAndAdd<EnergyCollectionSystem>()
                .OneFrame<EnergyCollectionCommand>()
                ;
        }
    }
}