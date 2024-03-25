using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IInputManager
    {
        float HorizontalDirection { get; }
        event Action OnFireRequired;
        event Action OnEscape;
    }

    public sealed class InputManager : MonoBehaviour, IInputManager
    {
        public float HorizontalDirection { get; private set; }

        public event Action OnFireRequired;
        public event Action OnEscape;

        void Update()
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