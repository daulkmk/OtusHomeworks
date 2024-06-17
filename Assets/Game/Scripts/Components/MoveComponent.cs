using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class MoveComponent
    {
        public AtomicVariable<Vector3> MoveDirectionWorld;
        public IAtomicValueObservable<Vector3> MoveDirectionLocal => _moveDirectionLocal;
        public AtomicVariable<bool> IsMoving;
        public Transform Root;
        
        [SerializeField] private float _speed = 3f;
        [SerializeField] private bool _moveInLocalDirection;
        [SerializeField] private bool _canMove;

        private readonly AtomicVariable<Vector3> _moveDirectionLocal = new();
        private readonly CompositeCondition _condition = new();

        public void Compose()
        {
            MoveDirectionWorld.Subscribe(moveDirection =>
            {
                IsMoving.Value = moveDirection != Vector3.zero;
            });
        }

        public void Update(float deltaTime)
        {
            if (_condition.IsTrue() && _canMove)
            {
                var direction = MoveDirectionWorld.Value;
                _moveDirectionLocal.Value = Root.InverseTransformDirection(direction);

                if (_moveInLocalDirection)
                    direction = _moveDirectionLocal.Value;
                
                Root.position += _speed * deltaTime * direction;
            }
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
    }
}