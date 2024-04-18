using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputMovementController : IFixedUpdatable
    {
        private readonly IInputManager _inputManager;
        private readonly IMove _moveComponent;

        public InputMovementController(IInputManager inputManager, IMove moveComponent)
        {
            _inputManager = inputManager;
            _moveComponent = moveComponent;
        }

        void IFixedUpdatable.OnFixedUpdate(float deltaTime)
        {
            var direction = new Vector2(_inputManager.HorizontalDirection, 0);
            _moveComponent.FixedMove(direction, deltaTime);
        }
    }
}