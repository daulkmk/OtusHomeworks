using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private readonly CompositeDisposable _disposables = new();

        private IStatPresenter _presenter;

        public void Show(IStatPresenter presenter)
        {
            Clear();

            _disposables.Add(_presenter = presenter);

            _presenter.Name.Subscribe(UpdateText).AddTo(_disposables);
            _presenter.Value.Subscribe(UpdateText).AddTo(_disposables);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Clear();
        }

        private void Clear() => _disposables.Clear();

        private void UpdateText(string _) => _text.text = _presenter.GetText();
    }
}