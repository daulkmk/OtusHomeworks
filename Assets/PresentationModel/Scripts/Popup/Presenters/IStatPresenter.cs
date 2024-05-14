using System;
using UniRx;

namespace ShootEmUp.PresentationModel
{
    public interface IStatPresenter : IDisposable
    {
        public IReadOnlyReactiveProperty<string> Name { get; }
        public IReadOnlyReactiveProperty<string> Value { get; }
    }
}