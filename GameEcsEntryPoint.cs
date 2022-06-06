using Ai;
using Ai.Camp;
using Attack;
using Block;
using Cameras;
using DELTation.LeoEcsExtensions.Composition;
using DELTation.LeoEcsExtensions.Composition.Di;
using Doors;
using Health;
using Health.Damage;
using Health.Death;
using Leopotam.EcsLite;
using Leveling;
using Leveling.Energy;
using Maps;
using Movement;
using Movement.Ragdoll;
using Movement.Roll;
using Spawning;
using Stun;
using TimeUtils;

public class GameEcsEntryPoint : EcsEntryPoint
{
    protected override void PopulateSystems(EcsSystems systems, EcsWorld world)
    {
        systems
            .CreateAndAdd<DestroyDeadBodiesOnGameRestartSystem>()
            .CreateAndAdd<DestroyCampEntitiesOnGameRestartSystem>()
            ;

        // Init
        systems
            .CreateAndAdd<PlayerSpawnSystem>()
            .CreateAndAdd<CampSpawnSystem>()
            .CreateAndAdd<MapGatesOpenOnStartSystem>()
            ;

        systems
            .OneFrame<GameRestartCommand>()
            ;

        // Timers
        systems
            .CreateAndAdd<TimerUpdateSystem>()
            ;

        // AI
        systems
            .CreateAndAdd<AgentBlockInputStartSystem>()
            .CreateAndAdd<AgentBlockInputUpdateSystem>()
            .CreateAndAdd<AgentSnapSystem>()
            .CreateAndAdd<AgentSpeedSystem>()
            .CreateAndAdd<PlayerFollowSystem>()
            .CreateAndAdd<AgentFollowDestinationSystem>()
            .CreateAndAddFeature<AgentReturnToCampFeature>()
            .CreateAndAdd<AgentSetDestinationSystem>()
            .OneFrame<NavMeshDestinationData>()
            ;

        // AI Input
        systems
            .CreateAndAdd<AgentDirectionSystem>()
            .CreateAndAdd<FollowAutoAttackSystem>()
            ;

        // Player Input
        systems
            .CreateAndAdd<PlayerDirectionSystem>()
            .CreateAndAdd<PlayerAttackInputSystem>()
            .CreateAndAdd<PlayerBlockInputSystem>()
            ;

        systems
            .CreateAndAdd<BlockStartStopSystem>()
            .CreateAndAdd<BlockUpdateSystem>()
            ;

        systems
            .CreateAndAdd<CharacterControllerGravitySystem>()
            .CreateAndAdd<SlopeDirectionCorrectionSystem>()
            .CreateAndAdd<StopDuringAttackSystem>()
            .CreateAndAdd<StopDuringBlockSystem>()
            .CreateAndAdd<StopDuringStunSystem>()
            .CreateAndAdd<RollRootMotionMultiplierSystem>()
            .CreateAndAdd<CharacterControllerMovementSystem>()
            .CreateAndAdd<PlayerRollSystem>()
            ;


        systems
            .CreateAndAdd<LookAtClosestEnemyRotationOverrideSystem>()
            .CreateAndAdd<FollowRotationOverrideSystem>()
            .CreateAndAdd<MovementRotationSystem>()
            .OneFrame<LookRotationOverrideData>()
            ;

        systems
            .CreateAndAdd<MovementAnimationSystem>()
            .CreateAndAdd<ExitIdleAnimationSystem>()
            ;

        systems
            .CreateAndAdd<ActiveAttackCooldownSystem>()
            .CreateAndAdd<AttackStartSystem>()
            .OneFrame<AttackStartCommand>()
            .CreateAndAdd<AttackUpdateSystem>()
            .CreateAndAdd<AttackAnimationSystem>()
            ;

        systems
            .CreateAndAdd<MeleeAttackSystem>()
            .CreateAndAdd<RangedAttackSystem>()
            .OneFrame<AttackActivationEvent>()
            ;

        systems
            .CreateAndAdd<RollInvincibilityTimeSystem>()
            .CreateAndAdd<RollInvincibilitySystem>()
            .CreateAndAdd<BlockDamageSystem>()
            .CreateAndAdd<DealDamageSystem>()
            .CreateAndAdd<ShakeCameraOnEnemyDamageSystem>()
            .CreateAndAdd<ActiveCameraShakeSystem>()
            .OneFrame<DealDamageCommand>()
            ;

        systems
            .CreateAndAdd<BlockStunSystem>()
            .CreateAndAdd<StunUpdateSystem>()
            ;

        // Death
        systems
            .CreateAndAdd<DeathCommandSystem>()
            .CreateAndAdd<DeathEnergyDropSystem>()
            .CreateAndAdd<PlayerDeathEnergyDropSystem>()
            .CreateAndAdd<PlayerDeathGameRestartSystem>()
            .CreateAndAdd<DeathAnimationSystem>()
            .CreateAndAdd<DeathRagdollSystem>()
            .CreateAndAdd<DeathRagdollImpulseSystem>()
            .CreateAndAdd<DeathHealthBarSystem>()
            .CreateAndAdd<DeadBodySystem>()
            .CreateAndAdd<DeathEntityDestroySystem>()
            .OneFrame<DeathCommand>() // sanity check
            ;

        systems
            .CreateAndAdd<DoorTriggerSystem>()
            .OneFrame<DoorTriggerEnterEvent>()
            ;

        systems
            .CreateAndAddFeature<StatsFeature>()
            ;

        // UI and VFX
        systems
            .CreateAndAdd<HealthBarSystem>()
            .CreateAndAdd<LookAtCameraSystem>()
            .CreateAndAdd<BlockVfxSystem>()
            .CreateAndAdd<DodgeVfxSystem>()
            ;

        systems
            .CreateAndAdd<RestartGameWhenTimerEndsSystem>()
            ;

        systems
            .OneFrame<FollowTargetIsOccludedTag>()
            .OneFrame<DamageImpulseData>()
            .OneFrame<BlockEvent>()
            .OneFrame<DodgeEvent>()
            .OneFrame<TimerEndedEvent>()
            .OneFrameUpdateEvents()
            ;
    }
}