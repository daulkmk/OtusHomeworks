using System;

namespace ShootEmUp
{
    public interface IInputManager
    {
        float HorizontalDirection { get; }
        event Action OnFireRequired;
        event Action OnEscape;
    }
}