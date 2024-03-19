using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IInputManager
    {
        float HorizontalDirection { get; }
        event Action OnFireRequired;
    }

    public sealed class InputManager : MonoBehaviour, IInputManager
    {
        public float HorizontalDirection { get; private set; }

        public event Action OnFireRequired;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnFireRequired?.Invoke();

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