using _Shared;
using Attack;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement.Roll;
using UnityEngine;

namespace Movement
{
    public class PlayerRollSystem : IEcsRunSystem
    {
        private static readonly int RollId = Animator.StringToHash("Roll");
        private readonly EcsFilter _filter;

        public PlayerRollSystem(EcsWorld world) =>
            _filter = world.Filter<RollData>()
                .Inc<PlayerTag>()
                .Inc<AnimatorData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                if (!CanStartRoll(i)) continue;
                if (!Input.GetButton("Jump")) continue;

                _filter.Get<AnimatorData>(i).Animator.SetTrigger(RollId);
                if (_filter.Has<IdleState>(i))
                    _filter.Del<IdleState>(i);
                ref var rollState = ref _filter.GetOrAdd<RollState>(i);
                rollState.CanRollAgain = false;
                rollState.RemainingInvincibilityTime = _filter.Read<RollData>(i).InvincibilityTime;
            }
        }

        private bool CanStartRoll(int i) =>
            _filter.Has<IdleState>(i) || _filter.Has<RollState>(i) && _filter.Read<RollState>(i).CanRollAgain;
    }
}