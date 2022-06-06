using _Shared.Utils;
using Attack;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

namespace Movement.Roll
{
    public class RollAnimationMediator : MonoBehaviour
    {
        private IEntityView _entityView;

        [Preserve]
        public void Construct(IEntityView entityView)
        {
            _entityView = entityView;
        }

        [UsedImplicitly]
        public void OnCanRoll()
        {
            if (_entityView.TryGetEntity(out var entity))
                entity.Get<RollState>().CanRollAgain = true;
        }

        public void OnExitedRoll()
        {
            if (_entityView.TryGetEntity(out var entity))
                entity.Replace<RollState, IdleState>();
        }
    }
}