using UniRx;

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

            _view.ExpProgressBarView.Show(presenter.ExpProgressBarPresenter);
            _view.DescriptionView.Show(presenter.DescriptionPresenter);
            _view.StatsView.Show(presenter.StatsPresenter);
            _view.LevelUpButtonView.Show(presenter.LevelUpCommand);

            _view.GameObject.SetActive(true);
        }

        public void Hide()
        {
            _view.ExpProgressBarView.Hide();
            _view.DescriptionView.Hide();
            _view.StatsView.Hide();
            _view.LevelUpButtonView.Hide();

            _view.GameObject.SetActive(false);
            _disposables.Clear();
        }
    }
}