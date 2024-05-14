using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public class PopupStatView : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private CompositeDisposable _disposables = new();

        public IStatPresenter Presenter { get; private set; }

        public void Show(IStatPresenter presenter)
        {
            if (Presenter != null)
                _disposables.Clear();

            _disposables.Add(Presenter = presenter);

            _disposables.Add(Presenter.Name.Subscribe(UpdateText));
            _disposables.Add(Presenter.Value.Subscribe(UpdateText));

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _disposables.Clear();
        }

        private void UpdateText(string _) => _text.text = $"{Presenter.Name.Value}: {Presenter.Value}";
    }
}