
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public class DescriptionView : MonoBehaviour, IDescriptionView
    {
        [field: SerializeField]
        public Text Name { get; private set; }

        [field: SerializeField] 
        public Text Description { get; private set; }

        [field: SerializeField] 
        public Text Level { get; private set; }

        [field: SerializeField] 
        public Image Icon { get; private set; }

        private readonly CompositeDisposable _disposables = new();

        public void Show(IDescriptionPresenter presenter)
        {
            Clear();

            presenter.Name.SubscribeToText(Name).AddTo(_disposables);
            presenter.Description.SubscribeToText(Description).AddTo(_disposables);
            presenter.Level.SubscribeToText(Level).AddTo(_disposables);
            presenter.Icon.Subscribe(SetIcon).AddTo(_disposables);
        }

        public void Hide() => Clear();

        private void Clear() => _disposables.Clear();

        private void SetIcon(Sprite sprite) => Icon.sprite = sprite;
    }
}