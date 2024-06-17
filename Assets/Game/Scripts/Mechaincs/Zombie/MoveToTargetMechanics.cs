using System;
using System.Collections;
using System.Collections.Generic;
using Atomic.Elements;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduction
{
    public class MoveToTargetMechanics
    {
        public IAtomicValueObservable<Vector3> Direction => _direction;
        public IAtomicValueObservable<bool> IsTargetReached => _isTargetReached;

        private readonly AtomicVariable<Vector3> _direction = new();
        private readonly AtomicVariable<bool> _isTargetReached = new();

        private readonly IAtomicValueObservable<Vector3> _targetPosition;
        private readonly IAtomicVariable<Vector3> _moveDirection;
        private readonly IAtomicValue<Vector3> _myPosition;
        private readonly float _minDistance;

        private readonly CompositeCondition _condition = new();

        
        public MoveToTargetMechanics(IAtomicVariable<Vector3> moveDirection
            , IAtomicValueObservable<Vector3> targetPosition
            , IAtomicValue<Vector3> myPosition
            , float minDistance)
        {
            _moveDirection = moveDirection;
            _targetPosition = targetPosition;
            _myPosition = myPosition;
            _minDistance = minDistance;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void Update()
        {
            var direction = _condition.IsTrue() ? _targetPosition.Value - _myPosition.Value : Vector3.zero;

            _isTargetReached.Value = direction.magnitude <= _minDistance;
            _direction.Value = direction.normalized;

            Move();            
        }

        private void Move()
        {
            Vector3 direction = Vector3.zero;
            if (!IsTargetReached.Value)
            {
                direction.x = Direction.Value.x;
                direction.z = Direction.Value.z;
            }
            _moveDirection.Value = direction;
        }
    }
}