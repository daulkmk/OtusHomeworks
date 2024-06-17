using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class AttackAnimationMechanics
    {
        private readonly Animator _animator;
        private readonly AnimatorDispatcher _animatorDispatcher;
        private readonly IAtomicObservable _attackRequest;
        private readonly IAtomicAction _attackAction;
        private readonly IAtomicValue<bool> _canAttack;

        private int _triggerHash = Animator.StringToHash("Attack");

        public AttackAnimationMechanics(
            Animator animator, 
            AnimatorDispatcher animatorDispatcher,
            IAtomicObservable attackRequest,
            IAtomicAction attackAction,
            IAtomicValue<bool> canFire)
        {
            _animator = animator;
            _animatorDispatcher = animatorDispatcher;
            _attackRequest = attackRequest;
            _attackAction = attackAction;
            _canAttack = canFire;
        }

        public void OnEnable()
        {
            _attackRequest.Subscribe(OnAttackRequested);
            _animatorDispatcher.SubscribeOnEvent("attack", _attackAction.Invoke);
        }

        public void OnDisable()
        {
            _attackRequest.Unsubscribe(OnAttackRequested);
            _animatorDispatcher.UnsubscribeOnEvent("attack", _attackAction.Invoke);
        }

        private void OnAttackRequested()
        {
            if (_canAttack.Value)
                _animator.SetTrigger(_triggerHash);
            else
                _animator.ResetTrigger(_triggerHash);
        }
    }
}