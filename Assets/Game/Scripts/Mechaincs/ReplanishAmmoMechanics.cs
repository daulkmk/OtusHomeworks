using Atomic.Elements;

namespace Lessons.Lesson_AtomicIntroduction
{
    public class ReplanishAmmoMechanics
    {
        private readonly int _interval;
        private readonly int _ammoAmount;
        private readonly AtomicVariable<int> _ammo;

        private float _timer = 0;

        public ReplanishAmmoMechanics(int interval, int ammoAmount, AtomicVariable<int> ammo)
        {
            _interval = interval;
            _ammoAmount = ammoAmount;
            _ammo = ammo;
        }

        public void Update(float dt)
        {
            _timer += dt;

            if (_timer > _interval)
            {
                _timer -= _interval;

                Replanish();
            }
        }

        private void Replanish()
        {
            _ammo.Value += _ammoAmount;
        }
    }
}