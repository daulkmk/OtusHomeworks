using Atomic.Elements;
using UnityEngine;

namespace Lessons.Lesson_AtomicIntroduction
{
    public class ConsumeAmmoMechanics
    {
        private readonly IAtomicVariable<int> _ammo;

        public ConsumeAmmoMechanics(AtomicEvent shootEvent, IAtomicVariable<int> ammo)
        {
            _ammo = ammo;

            shootEvent.Subscribe(OnShoot);
        }

        private void OnShoot()
        {
            if (_ammo.Value > 0)
                _ammo.Value -= 1;
        }
    }
}