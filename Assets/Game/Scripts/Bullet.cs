using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_Components.Components;
using UnityEngine;

namespace Lessons.Lesson_Components.Scripts
{
    //Facade
    public class Bullet : AtomicEntity
    {
        [Get(MoveAPI.MoveDirection)]
        public IAtomicVariable<Vector3> MoveDirection => MoveComponent.MoveDirectionWorld;
        
        [SerializeField] private int _damage = 1;
        [SerializeField] private MoveComponent MoveComponent;

        private bool _isDestroyInProgress = false;
        
        private void Awake()
        {
            MoveComponent.Compose();
        }

        private void Update()
        {
            MoveComponent.Update(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isDestroyInProgress)
                return;

            if (other.TryGetComponent(out IAtomicEntity atomicEntity))
            {
                if (atomicEntity.TryGet<IAtomicAction<int>>(LifeAPI.TakeDamageAction, out var action))
                {
                    action.Invoke(_damage);

                    _isDestroyInProgress = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}