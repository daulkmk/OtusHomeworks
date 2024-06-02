using System;
using UniRx;

namespace ShootEmUp.PresentationModel
{
    public interface IPopupPresenter : IDisposable
    {
        IDescriptionPresenter DescriptionPresenter { get; }
        IStatsPresenter StatsPresenter { get; }
        IExpProgressBarPresenter ExpProgressBarPresenter{ get; }
        ReactiveCommand LevelUpCommand { get; }
    }
}