using UniRx;
using UnityEngine;

namespace ShootEmUp.PresentationModel
{
    public interface IDescriptionPresenter
    {
        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
    }
}