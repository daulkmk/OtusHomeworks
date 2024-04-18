using System;
using UnityEngine;

namespace ShootEmUp
{
    public class OnDisableEvent : MonoBehaviour
    {
        public event Action OnDisabled;

        private void OnDisable()
        {
            OnDisabled?.Invoke();
        }
    }
}