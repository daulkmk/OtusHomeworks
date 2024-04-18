using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputWeaponController : IPauseGameListener
    {
        private readonly IWeapon _weapon;
        private readonly IInputManager _inputManager;

        public InputWeaponController(IInputManager inputManager, IWeapon weapon)
        {
            _weapon = weapon;
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