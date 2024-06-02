using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public class ExpProgressBarView : MonoBehaviour, IProgressBarView
    {
        [SerializeField] private Image _progress;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _filledSprite;
        [SerializeField] private Text _text;

        private IExpProgressBarPresenter _presenter;
        private int _maxValue;
        private readonly CompositeDisposable _disposables = new();

        public void Show(IExpProgressBarPresenter presenter)
        {
            Clear();

            _presenter = presenter;

            _presenter.RequiredExperience.Subscribe(OnRequiredExpChanged).AddTo(_disposables);
            _presenter.Experience.Subscribe(UpdateProgress).AddTo(_disposables);

            UpdateExpProgress();
        }

        public void Hide() => Clear();

        private void Clear() => _disposables.Clear();

        private void OnRequiredExpChanged(int _) => UpdateExpProgress();

        private void UpdateExpProgress()
        {
            UpdateProgress(_presenter.Experience.Value, _presenter.RequiredExperience.Value);
        }

        private void UpdateProgress(int progress, int maxValue)
        {
            _maxValue = maxValue;
            UpdateProgress(progress);
        }

        private void UpdateProgress(int progress)
        {
            _progress.fillAmount = progress / (float)_maxValue;

            UpdateSprite(progress);
            _text.text = _presenter.GetText(progress, _maxValue);
        }

        private void UpdateSprite(int progress)
        {
            _progress.sprite = progress >= _maxValue ? _filledSprite : _normalSprite;
        }
    }
}