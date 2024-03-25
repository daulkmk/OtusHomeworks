using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputWeaponController : MonoBehaviour, IPauseGameListener
    {
        [SerializeField] private IWeaponComponent _weapon;
        [SerializeField] private IInputManager _inputManager;

        public void Initialize(IInputManager inputManager, IWeaponComponent weaponComponent)
        {
            _weapon = weaponComponent;
            _inputManager = inputManager;
        }

        private void OnFireRequired()
        {
            var direction = _weapon.Position + Vector2.up;
            _weapon.Fire(direction);
        }

        void IPauseGameListener.OnGamePaused(bool paused)
        {
            if (paused)
                _inputManager.OnFireRequired -= OnFireRequired;
            else
                _inputManager.OnFireRequired += OnFireRequired;
        }
    }
}