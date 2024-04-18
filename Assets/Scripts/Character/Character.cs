using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Character : ICharacter, IInitializable, IDisposable, IDamagable
    {
        public bool IsPlayer { get; private set; }

        public IMove Move { get; private set; }
        public IWeapon Weapon { get; private set; }
        public IHitPoints HitPoints { get; private set; }

        public event Action<Character> OnDeath;

        public IGameObject GameObject { get; private set; }

        public Character(IWeapon weapon, IMove move, IHitPoints hitPoints, IGameObject gameObject, bool isPlayer)
        {
            GameObject = gameObject;

            Move = move;
            Weapon = weapon;
            HitPoints = hitPoints;
            IsPlayer = isPlayer;
        }

        void IInitializable.Initialize()
        {
            Initialize();
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        protected virtual void Initialize()
        {
            UpdateComponents();

            HitPoints.Restore();
            HitPoints.OnEmptyHP += OnEmptyHP;
            HitPoints.OnRestoreHP += OnRestoreHP;

            GameObject.OnEnable += UpdateComponents;
            GameObject.OnDisable += UpdateComponents;
        }

        protected virtual void Dispose()
        {
            HitPoints.OnEmptyHP -= OnEmptyHP;
            HitPoints.OnRestoreHP -= OnRestoreHP;

            GameObject.OnEnable -= UpdateComponents;
            GameObject.OnDisable -= UpdateComponents;
        }

        protected virtual bool CanFireWeapon() => GameObject.IsActiveInHierarchy && HitPoints.IsHitPointsExists;
        protected virtual bool CanMove() => GameObject.IsActiveInHierarchy && HitPoints.IsHitPointsExists;

        private void OnEmptyHP()
        {
            Weapon.CanFire.Value = CanFireWeapon();

            OnDeath?.Invoke(this);
        }

        private void OnRestoreHP()
        {
            UpdateComponents();
        }

        private void UpdateComponents()
        {
            Weapon.CanFire.Value = CanFireWeapon();
            Move.CanMove.Value = CanMove();
        }

        public void SetPosition(Vector3 position)
        {
            GameObject.Transform.Position = position;
        }

        public void ApplyDamage(int damage, bool isPlayer)
        {
            if (isPlayer == IsPlayer)
                return;
            if (!HitPoints.IsHitPointsExists)
                return;

            HitPoints.AddValue(-damage);
        }
    }
}