using UniRx;

namespace ShootEmUp.PresentationModel
{
    public interface IExpProgressBarPresenter
    {
        IReadOnlyReactiveProperty<int> Experience { get; }
        IReadOnlyReactiveProperty<int> RequiredExperience { get; }
        string GetText(int progress, int maxValue);
    }
}