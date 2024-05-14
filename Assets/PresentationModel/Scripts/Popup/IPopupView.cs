using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public interface IPopupView
    {
        Text Name { get; }
        Text Description { get; }
        Text Level { get; }
        Image Icon { get; }
        ProgressBar ExpProgressBar { get; }
        LevelUpButton LevelUpButton { get; }
        StatsList StatsList { get; }
        Button CloseButton { get; }
        GameObject GameObject{ get; }
    }
}