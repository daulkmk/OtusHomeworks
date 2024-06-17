using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class ZombieVfx
    {
        [SerializeField] private ParticleSystem _takeDamageVfx;
        
        private ZombieCore _core;

        public void Compose(ZombieCore core)
        {
            _core = core;
        }

        public void OnEnable()
        {
            _core.LifeComponent.TakeDamageEvent.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _core.LifeComponent.TakeDamageEvent.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int damage)
        {
            _takeDamageVfx.Play();
        }
    }
}