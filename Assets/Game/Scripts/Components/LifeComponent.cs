using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Components.Components
{
    [Serializable]
    public class LifeComponent
    {
        public AtomicEvent<int> TakeDamageAction;
        public AtomicEvent<int> TakeDamageEvent;
        public IAtomicValueObservable<bool> IsDead => _isDead;
        public IAtomicValueObservable<int> HitPoints => _hitPoints;


        [SerializeField] private AtomicVariable<int> _hitPoints = new(10);
        private readonly AtomicVariable<bool> _isDead = new();

        public void Compose()
        {
            TakeDamageAction.Subscribe(TakeDamage);
        }
        
        public bool IsAlive()
        {
            return !IsDead.Value;
        }
        
        [Button]
        public void TakeDamage(int damage)
        {
            if (IsDead.Value)
            {
                return;
            }
            
            _hitPoints.Value -= damage;
            TakeDamageEvent.Invoke(damage);
            Debug.Log($"Take damage = {damage}");
            
            if (_hitPoints.Value <= 0)
            {
                _isDead.Value = true;
            }
        }
    }
}