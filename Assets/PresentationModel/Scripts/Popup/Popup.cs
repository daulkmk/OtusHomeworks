using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public class Popup : IPopup
    {
        private readonly IPopupView _view;
        private readonly CompositeDisposable _disposables = new();

        private IPopupPresenter _presenter = null;

        public Popup(IPopupView view)
        {
            _view = view;
            _view.CloseButton.onClick.AddListener(Hide);
        }

        public void Show(IPopupPresenter presenter)
        {
            if (_presenter != null)
                _disposables.Clear();

            _disposables.Add(_presenter = presenter);

            _disposables.Add(_presenter.Name.SubscribeToText(_view.Name));
            _disposables.Add(_presenter.Description.SubscribeToText(_view.Description));
            _disposables.Add(_presenter.Level.SubscribeToText(_view.Level));
            _disposables.Add(_presenter.Icon.Subscribe(SetIcon));
            _disposables.Add(_presenter.RequiredExperience.Subscribe(OnRequiredExpChanged));
            _disposables.Add(_presenter.Experience.Subscribe(OnExpChanged));

            _disposables.Add(_presenter.LevelUpCommand.BindTo(_view.LevelUpButton.Button));
            _disposables.Add(_presenter.LevelUpCommand.CanExecute.Subscribe(UpdateButtonState));
            _disposables.Add(_presenter.Stats.ObserveAdd().Subscribe(OnStatAdded));
            _disposables.Add(_presenter.Stats.ObserveRemove().Subscribe(OnStatRemoved));

            UpdateButtonState(_presenter.LevelUpCommand.CanExecute.Value);
            UpdateExpProgress();

            _view.StatsList.Clear();
            foreach (var stat in _presenter.Stats)
                _view.StatsList.Show(stat);

            _view.GameObject.SetActive(true);
        }

        private void OnStatRemoved(CollectionRemoveEvent<IStatPresenter> @event) => _view.StatsList.Hide(@event.Value);
        private void OnStatAdded(CollectionAddEvent<IStatPresenter> @event) => _view.StatsList.Show(@event.Value);

        public void Hide()
        {
            _view.GameObject.SetActive(false);
            _disposables.Clear();
        }
        private void OnExpChanged(int exp) => _view.ExpProgressBar.UpdateProgress(exp);
        private void OnRequiredExpChanged(int _) => UpdateExpProgress();

        private void UpdateExpProgress()
        {
            _view.ExpProgressBar.UpdateProgress(_presenter.Experience.Value, _presenter.RequiredExperience.Value);
        }

        private void UpdateButtonState(bool canExecute) => _view.LevelUpButton.SetAvailable(canExecute);

        private void SetIcon(Sprite sprite) => _view.Icon.sprite = sprite;
    }
}