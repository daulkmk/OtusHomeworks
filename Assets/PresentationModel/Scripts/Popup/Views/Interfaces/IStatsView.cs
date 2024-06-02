namespace ShootEmUp.PresentationModel
{
    public interface IStatsView
    {
        void Show(IStatsPresenter statsPresenter);
        void Hide();
    }
}