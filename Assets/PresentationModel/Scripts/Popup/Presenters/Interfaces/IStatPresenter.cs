using System;
using UniRx;

namespace ShootEmUp.PresentationModel
{
    public interface IStatPresenter : IDisposable
    {
        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Value { get; }
        string GetText();
    }
}