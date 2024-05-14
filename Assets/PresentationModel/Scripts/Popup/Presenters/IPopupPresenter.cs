using System;
using UniRx;
using UnityEngine;

namespace ShootEmUp.PresentationModel
{
    public interface IPopupPresenter : IDisposable
    {
        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
        IReadOnlyReactiveProperty<int> Experience { get; }
        IReadOnlyReactiveProperty<int> RequiredExperience { get; }
        IReadOnlyReactiveCollection<IStatPresenter> Stats { get; }

        ReactiveCommand LevelUpCommand { get; }
    }
}