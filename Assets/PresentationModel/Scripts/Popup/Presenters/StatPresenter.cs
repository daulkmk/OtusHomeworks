using System;
using Lessons.Architecture.PM;
using UniRx;

namespace ShootEmUp.PresentationModel
{
    public class StatPresenter : IStatPresenter
    {
        public IReadOnlyReactiveProperty<string> Name { get; private set; }
        public IReadOnlyReactiveProperty<string> Value => _value;

        private readonly ReactiveProperty<string> _value;
        private readonly CharacterStat _stat;
        
        public StatPresenter(CharacterStat characterStat)
        {
            _stat = characterStat;

            Name = new ReactiveProperty<string>(_stat.Name);

            _value = new ReactiveProperty<string>(_stat.Value.ToString());
            _stat.OnValueChanged += OnValueChanged;
        }

        private void OnValueChanged(int value) => _value.Value = value.ToString();

        public void Dispose()
        {
            _stat.OnValueChanged -= OnValueChanged;
        }
    }
}