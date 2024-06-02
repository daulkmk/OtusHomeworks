
namespace ShootEmUp.PresentationModel
{
    public interface IProgressBarView
    {
        void Show(IExpProgressBarPresenter presenter);
        void Hide();
    }
}