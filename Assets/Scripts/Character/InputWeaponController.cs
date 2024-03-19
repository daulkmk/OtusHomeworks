using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputWeaponController : MonoBehaviour
    {
        [SerializeField] private IWeaponComponent _weapon;

        public void Initialize(IInputManager inputManager, IWeaponComponent weaponComponent)
        {
            _weapon = weaponComponent;
            inputManager.OnFireRequired += OnFireRequired;
        }

        private void OnFireRequired()
        {
            var direction = _weapon.Position + Vector2.up;
            _weapon.Fire(direction);
        }
    }
}