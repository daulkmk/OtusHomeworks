using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class ZombieAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;
        
        private ZombieCore _core;

        private MoveAnimationMechanics _moveAnimationMechanics;
        private BoolAnimationMechanics _boolAnimationMechanics;
        private AttackAnimationMechanics _attackAnimationMechanics;
        
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public void Compose(ZombieCore characterCore)
        {
            _core = characterCore;
            
            _moveAnimationMechanics =
                new MoveAnimationMechanics(moveDirection: _core.MoveComponent.MoveDirectionLocal
                    , animator: _animator
                );
            _boolAnimationMechanics =
                new BoolAnimationMechanics(value: _core.LifeComponent.IsDead
                    , animator: _animator
                    , animatorKey: IsDead
                );
            _attackAnimationMechanics =
                new AttackAnimationMechanics(animator: _animator
                    , animatorDispatcher: _animatorDispatcher
                    , attackRequest: _core.PunchComponent.AttackRequest
                    , attackAction: _core.PunchComponent.AttackAction
                    , canFire: _core.PunchComponent.CanAttack
                );
        }

        public void OnEnable()
        {
            _moveAnimationMechanics.OnEnable();
            _boolAnimationMechanics.OnEnable();
            _attackAnimationMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _moveAnimationMechanics.OnDisable();
            _boolAnimationMechanics.OnDisable();
            _attackAnimationMechanics.OnDisable();
        }
    }
}