using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputMovementController : MonoBehaviour, IFixedUpdatable
    {
        [Inject] private IInputManager _inputManager;
        [Inject] private IMoveComponent _moveComponent;

        void IFixedUpdatable.OnFixedUpdate(float deltaTime)
        {
            var direction = new Vector2(_inputManager.HorizontalDirection, 0);
            _moveComponent.FixedMove(direction);
        }
    }
}