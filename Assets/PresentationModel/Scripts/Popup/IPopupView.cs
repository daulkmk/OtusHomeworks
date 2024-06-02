using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public interface IPopupView
    {
        IProgressBarView ExpProgressBarView { get; }
        IDescriptionView DescriptionView { get; }
        IStatsView StatsView { get; }

        IButtonView LevelUpButtonView { get; }

        Button CloseButton { get; }
        GameObject GameObject { get; }
    }
}