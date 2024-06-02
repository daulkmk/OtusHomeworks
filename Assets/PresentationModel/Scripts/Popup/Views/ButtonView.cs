using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public sealed class ButtonView : MonoBehaviour, IButtonView
    {
        [SerializeField] private Button _button;

        [Space]
        [SerializeField] private Image _buttonBackground;

        [SerializeField] private Sprite _availableButtonSprite;

        [SerializeField] private Sprite _lockedButtonSprite;

        private readonly CompositeDisposable _disposables = new();

        public void Show(ReactiveCommand command)
        {
            Clear();

            command.BindTo(_button).AddTo(_disposables);
            command.CanExecute.Subscribe(UpdateButtonState).AddTo(_disposables);

            UpdateButtonState(command.CanExecute.Value);
        }

        public void Hide() => Clear();

        private void Clear() => _disposables.Clear();

        private void UpdateButtonState(bool canExecute) => SetAvailable(canExecute);

        private void SetAvailable(bool isAvailable)
        {
            var state = isAvailable ? ButtonState.Available : ButtonState.Locked;
            SetState(state);
        }

        private void SetState(ButtonState state)
        {
            switch (state)
            {
                case ButtonState.Available:
                    _button.interactable = true;
                    _buttonBackground.sprite = _availableButtonSprite;
                    break;
                case ButtonState.Locked:
                    _button.interactable = false;
                    _buttonBackground.sprite = _lockedButtonSprite;
                    break;
                default:
                    throw new Exception($"Undefined button state {state}!");
            }
        }
    }
}
