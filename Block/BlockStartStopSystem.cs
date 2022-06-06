using _Shared.Utils;
using Attack;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using Movement;
using UnityEngine;

namespace Block
{
    public class BlockStartStopSystem : IEcsRunSystem
    {
        private static readonly int BlockId = Animator.StringToHash("Block");
        private readonly EcsFilter _startFilter;
        private readonly EcsFilter _stopFilter;

        public BlockStartStopSystem(EcsWorld world)
        {
            _startFilter = world.Filter<AnimatorData>()
                .Inc<IdleState>()
                .Inc<BlockInputTag>()
                .End();
            _stopFilter = world.Filter<AnimatorData>()
                .Inc<BlockState>()
                .Exc<BlockInputTag>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _startFilter)
            {
                _startFilter.Replace<IdleState, BlockState>(i);
                _startFilter.Get<AnimatorData>(i).Animator.SetBool(BlockId, true);
            }

            foreach (var i in _stopFilter)
            {
                _stopFilter.Replace<BlockState, IdleState>(i);
                _stopFilter.Get<AnimatorData>(i).Animator.SetBool(BlockId, false);
                _stopFilter.Del<BlockActiveTag>(i);
            }
        }
    }
}