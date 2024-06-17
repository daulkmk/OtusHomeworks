using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveAnimationMechanics
    {
        private static readonly int Forward = Animator.StringToHash("forward");
        private static readonly int Right = Animator.StringToHash("right");

        private readonly IAtomicObservable<Vector3> _moveDirection;
        private readonly Animator _animator;

        public MoveAnimationMechanics(
            IAtomicObservable<Vector3> moveDirection, 
            Animator animator)
        {
            _moveDirection = moveDirection;
            _animator = animator;
        }

        public void OnEnable()
        {
            _moveDirection.Subscribe(OnMoveDirectionChanged);
        }

        public void OnDisable()
        {
            _moveDirection.Unsubscribe(OnMoveDirectionChanged);
        }
        
        private void OnMoveDirectionChanged(Vector3 moveDirection)
        {
            _animator.SetFloat(Forward, moveDirection.z);
            _animator.SetFloat(Right, moveDirection.x);
        }
    }
}