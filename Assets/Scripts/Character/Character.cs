using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Character : MonoBehaviour, IDamagable
    {
        [SerializeField] protected HitPointsComponent _hitPoints;
        [SerializeField] protected MoveComponent _moveComponent;
        [SerializeField] protected WeaponComponent _weapon;

        [field: SerializeField]
        public bool IsPlayer { get; private set; }

        public IMoveComponent MoveComponent => _moveComponent;
        public IWeaponComponent WeaponComponent => _weapon;
        public IHitPoints HitPoints => _hitPoints;

        public event Action<Character> OnDeath;

        //TODO direct injection into weapon
        public void Initialize(IBulletSystem bulletSystem)
        {
            _weapon.Initialize(bulletSystem);
        }

        protected virtual void Awake()
        {
            _weapon.IsPlayer = IsPlayer;
            _weapon.CanFireDelegate = CanFireWeapon;

            _moveComponent.CanMoveDelegate = CanMove;

            _hitPoints.Restore();
            _hitPoints.OnEmptyHP += OnEmptyHP;
        }

        protected virtual bool CanFireWeapon() => gameObject.activeInHierarchy && _hitPoints.IsHitPointsExists;
        protected virtual bool CanMove() => gameObject.activeInHierarchy && _hitPoints.IsHitPointsExists;

        private void OnEmptyHP()
        {
            OnDeath?.Invoke(this);
        }

        public void ApplyDamage(int damage, bool isPlayer)
        {
            if (isPlayer == IsPlayer)
                return;
            if (!_hitPoints.IsHitPointsExists)
                return;

            _hitPoints.AddValue(-damage);
        }
    }
}