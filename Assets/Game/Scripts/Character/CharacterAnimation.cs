using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class CharacterAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;
        
        private CharacterCore _core;

        private MoveAnimationMechanics _moveAnimationMechanics;
        private BoolAnimationMechanics _boolAnimationMechanics;
        private AttackAnimationMechanics _shootAnimationMechanics;
        
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public void Compose(CharacterCore characterCore)
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
            _shootAnimationMechanics =
                new AttackAnimationMechanics(animator: _animator
                    , animatorDispatcher: _animatorDispatcher
                    , attackRequest: _core.ShootComponent.ShootRequest
                    , attackAction: _core.ShootComponent.ShootAction
                    , canFire: _core.ShootComponent.CanFire
                );
        }

        public void OnEnable()
        {
            _moveAnimationMechanics.OnEnable();
            _boolAnimationMechanics.OnEnable();
            _shootAnimationMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _moveAnimationMechanics.OnDisable();
            _boolAnimationMechanics.OnDisable();
            _shootAnimationMechanics.OnDisable();
        }
    }
}