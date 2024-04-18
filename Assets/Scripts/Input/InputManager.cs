using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputManager : IInputManager, ITickable
    {
        public float HorizontalDirection { get; private set; }

        public event Action OnFireRequired;
        public event Action OnEscape;

        void ITickable.Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnFireRequired?.Invoke();

            if (Input.GetKeyDown(KeyCode.Escape))
                OnEscape?.Invoke();

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }
        }
    }
}