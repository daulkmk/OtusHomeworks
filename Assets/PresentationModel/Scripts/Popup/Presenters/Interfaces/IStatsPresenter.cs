using UniRx;

namespace ShootEmUp.PresentationModel
{
    public interface IStatsPresenter
    {
        IReadOnlyReactiveCollection<IStatPresenter> Stats { get; }
    }
}