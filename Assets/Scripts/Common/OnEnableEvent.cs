using System;
using UnityEngine;

namespace ShootEmUp
{
    public class OnEnableEvent : MonoBehaviour
    {
        public event Action OnEnabled;

        private void OnEnable()
        {
            OnEnabled?.Invoke();
        }
    }
}