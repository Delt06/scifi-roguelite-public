using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Attack
{
    public class AttackUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public AttackUpdateSystem(EcsWorld world) =>
            _filter = world.Filter<AttackState>()
                .Inc<AttackData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var activeAttackData = ref _filter.Get<AttackState>(i);
                activeAttackData.ElapsedTime += Time.deltaTime;

                if (!activeAttackData.Activated)
                {
                    var activationTime = _filter.Read<AttackData>(i).ActivationTime;
                    var normalizedTime = activeAttackData.GetNormalizedTime();
                    if (normalizedTime >= activationTime)
                    {
                        _filter.Add<AttackActivationEvent>(i);
                        activeAttackData.Activated = true;
                    }
                }


                if (activeAttackData.ElapsedTime < activeAttackData.Duration) continue;
                AttackUtils.StopAttackAndTransitionTo<IdleState>(_filter, i);
            }
        }
    }
}