using _Shared.Utils;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Attack
{
    public static class AttackUtils
    {
        public static ref TNewState StopAttackAndTransitionTo<TNewState>(EcsFilter filter, int idx)
            where TNewState : struct
        {
            ref var newState = ref filter.Replace<AttackState, TNewState>(idx);
            ref var activeAttackCooldown = ref filter.Add<ActiveAttackCooldown>(idx);
            activeAttackCooldown.RemainingTime = filter.Read<AttackData>(idx).Cooldown;
            return ref newState;
        }
    }
}