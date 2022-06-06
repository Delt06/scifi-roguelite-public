using _Shared;
using Attack;
using Camps;
using DELTation.DIFramework;
using DELTation.DIFramework.Containers;
using DELTation.LeoEcsExtensions.Composition;
using DELTation.LeoEcsExtensions.Composition.Di;
using Health;
using Leveling;
using Leveling.Energy;
using Leveling.Raw;
using Maps;
using Movement;
using Sirenix.OdinInspector;
using Spawning;
using UnityEngine;
using Vfx;

[RequireComponent(typeof(EcsDependencyContainer))]
public class DiCompositionRoot : DependencyContainerBase, IStaticDataProvider
{
    [SerializeField] [Required] private EcsEntryPoint _ecsEntryPoint;
    [SerializeField] [Required] private SceneData _sceneData;
    [SerializeField] [Required] private MovementStaticData _movementStaticData;
    [SerializeField] [Required] private HealthStaticData _healthStaticData;
    [SerializeField] [Required] private AttackStaticData _attackStaticData;
    [SerializeField] [Required] private VfxStaticData _vfxStaticData;
    [SerializeField] [Required] private EnemyStaticData _enemyStaticData;
    [SerializeField] [Required] private LevelingStaticData _levelingStaticData;
    [SerializeField] [Required] private SpawningStaticData _spawningStaticData;

    public EnemyStaticData EnemyStaticData => _enemyStaticData;

    protected override void ComposeDependencies(ContainerBuilder builder)
    {
        builder
            .RegisterIfNotNull(_ecsEntryPoint)
            .RegisterIfNotNull(_sceneData)
            .RegisterIfNotNull(_movementStaticData)
            .RegisterIfNotNull(_healthStaticData)
            .RegisterIfNotNull(_attackStaticData)
            .RegisterIfNotNull(_vfxStaticData)
            .Register<VfxFactory>()
            .RegisterIfNotNull(_enemyStaticData)
            .Register<EnemyFactory>()
            .Register<PrefsRawPlayerProgress>()
            .Register<PlayerExperience>()
            .RegisterIfNotNull(_levelingStaticData)
            .Register<PlayerStats>()
            .Register<CollectablesFactory>()
            .RegisterIfNotNull(_spawningStaticData)
            .Register<GateService>()
            ;
    }
}