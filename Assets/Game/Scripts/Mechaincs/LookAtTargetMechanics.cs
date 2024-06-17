using System;
using Atomic.Elements;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduction
{
    public class LookAtTargetMechanics
    {
        public IAtomicVariableObservable<Vector3> TargetPoint { get; private set; } = new AtomicVariable<Vector3>();

        private readonly IAtomicAction<Vector3> _rotateAction;
        private readonly IAtomicValue<Vector3> _transform;
        private readonly CompositeCondition _condition = new();

        public LookAtTargetMechanics(
            
            IAtomicAction<Vector3> rotateAction,
            IAtomicValue<Vector3> transform)
        {
            _rotateAction = rotateAction;
            _transform = transform;
        }

        public void Update()
        {
            if (!_condition.IsTrue())
            {
                return;
            }
            
            var direction = TargetPoint.Value - _transform.Value;
            direction.y = 0; //Rotate only aroung Y axis
            
            _rotateAction.Invoke(direction);
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}