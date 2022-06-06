using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views;
using UnityEngine;
using UnityEngine.Scripting;

namespace Movement
{
    [RequireComponent(typeof(Animator))]
    public class RootMotionView : MonoBehaviour
    {
        private Animator _animator;
        private IEntityView _entityView;

        [Preserve]
        public void Construct(IEntityView entityView)
        {
            _entityView = entityView;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            if (_entityView.TryGetEntity(out var entity))
                entity.Get<MovementData>().RootMotion = _animator.deltaPosition;
        }
    }
}