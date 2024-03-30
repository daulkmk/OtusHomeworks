using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputWeaponController : MonoBehaviour, IPauseGameListener
    {
        [Inject] private IWeaponComponent _weapon;
        [Inject] private IInputManager _inputManager;

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