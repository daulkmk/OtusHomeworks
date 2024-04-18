using R3;

namespace ShootEmUp
{
    public sealed class AttackAgent : IAttackAgent, IUpdatable
    {
        private readonly IWeapon _weapon;
        private readonly float _countdown;

        private ITransform _target;
        private float _currentTime;

        public ReactiveProperty<bool> CanAttack { get; }

        public AttackAgent(IWeapon weapon, float countdown)
        {
            CanAttack = new ReactiveProperty<bool>(false);

            _weapon = weapon;
            _countdown = countdown;
        }

        public void SetTarget(ITransform target)
        {
            _target = target;
        }

        public void Reset()
        {
            _currentTime = _countdown;
        }

        void IUpdatable.OnUpdate(float deltaTime)
        {
            if (!CanAttack.Value)
                return;
            
            _currentTime -= deltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }

        private void Fire()
        {
            _weapon.Fire(_target.Position);
        }
    }
}