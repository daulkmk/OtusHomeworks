using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.UI
{
    public class SingleButtonScreen : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private Action onActionRequest;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        public void Show(Action onActionRequested)
        {
            onActionRequest = onActionRequested;
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);

        private void OnButtonClick()
        {
            onActionRequest?.Invoke();
        }
    }
}