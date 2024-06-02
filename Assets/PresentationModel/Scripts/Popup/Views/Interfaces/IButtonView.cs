using UniRx;

namespace ShootEmUp.PresentationModel
{
    public interface IButtonView 
    {
        void Show(ReactiveCommand command);
        void Hide();
    }
}