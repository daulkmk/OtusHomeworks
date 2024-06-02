using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public interface IDescriptionView
    {
        Text Name { get; }
        Text Description { get; }
        Text Level { get; }
        Image Icon { get; }

        void Show(IDescriptionPresenter presenter);
        void Hide();
    }
}