using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class CharacterAudio
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _shootSfx;
        [SerializeField] private AudioClip _getHitSfx;
        
        private CharacterCore _core;

        public void Compose(CharacterCore core)
        {
            _core = core;
        }

        public void OnEnable()
        {
            _core.ShootComponent.ShootEvent.Subscribe(OnShoot);
            _core.LifeComponent.TakeDamageEvent.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _core.ShootComponent.ShootEvent.Unsubscribe(OnShoot);
            _core.LifeComponent.TakeDamageEvent.Unsubscribe(OnTakeDamage);
        }
        
        private void OnShoot() => Play(_shootSfx);
        private void OnTakeDamage(int value) =>Play(_getHitSfx);

        private void Play(AudioClip clip) => _audioSource.PlayOneShot(clip);
    }
}