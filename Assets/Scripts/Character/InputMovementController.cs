using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputMovementController : MonoBehaviour
    {
        private IInputManager _inputManager;
        private IMoveComponent _moveComponent;

        public void Initialize(IInputManager inputManager, IMoveComponent moveComponent)
        {
            _inputManager = inputManager;
            _moveComponent = moveComponent;
        }

        private void FixedUpdate()
        {
            var direction = new Vector2(_inputManager.HorizontalDirection, 0);
            _moveComponent.FixedMove(direction);
        }
    }
}