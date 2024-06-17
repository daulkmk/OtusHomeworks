using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    [Serializable]
    public class ZombieAudio
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _punchSfx;
        [SerializeField] private AudioClip _deathSfx;
        
        private ZombieCore _core;

        public void Compose(ZombieCore core)
        {
            _core = core;
        }

        public void OnEnable()
        {
            _core.PunchComponent.AttackEvent.Subscribe(OnPunched);
            _core.LifeComponent.IsDead.Subscribe(OnIsDead);
        }

        public void OnDisable()
        {
            _core.PunchComponent.AttackEvent.Unsubscribe(OnPunched);
            _core.LifeComponent.IsDead.Unsubscribe(OnIsDead);
        }

        private void OnPunched() => Play(_punchSfx);

        private void OnIsDead(bool isDead)
        {
            if (isDead)
                Play(_deathSfx);
        }

        private void Play(AudioClip clip) => _audioSource.PlayOneShot(clip);

    }
}