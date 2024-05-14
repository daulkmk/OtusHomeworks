using System;
using Lessons.Architecture.PM;
using UniRx;
using UnityEditor;

namespace ShootEmUp.PresentationModel
{
    public class PlayerLevelPresenter : IDisposable
    {
        public ReactiveCommand LevelUpCommand { get; private set; }
        public IReadOnlyReactiveProperty<int> Experience => _experience;
        public IReadOnlyReactiveProperty<int> RequiredExperience => _requiredExperience;
        public IReadOnlyReactiveProperty<string> Level => _level;

        private readonly ReactiveProperty<int> _experience;
        private readonly ReactiveProperty<int> _requiredExperience;
        private readonly ReactiveProperty<string> _level;

        private readonly PlayerLevel _playerLevel;
        private readonly IDisposable _subcription;

        public PlayerLevelPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _experience = new ReactiveProperty<int>(playerLevel.CurrentExperience);
            _playerLevel.OnExperienceChanged += OnExperienceChanged;

            _requiredExperience = new ReactiveProperty<int>(playerLevel.RequiredExperience);

            _level = new ReactiveProperty<string>(_playerLevel.CurrentLevel.ToString());
            _playerLevel.OnLevelUp += OnLevelUp;

            LevelUpCommand = Observable.Merge(_experience, _requiredExperience).Select(x => _playerLevel.CanLevelUp()).ToReactiveCommand();
            _subcription = LevelUpCommand.Subscribe(OnLevelUpCommand);
        }

        private void OnLevelUpCommand(Unit _) => _playerLevel.LevelUp();

        private void OnExperienceChanged(int exp) => _experience.Value = exp;
        private void OnLevelUp()
        {
            _level.Value = _playerLevel.CurrentLevel.ToString();
            _requiredExperience.Value = _playerLevel.RequiredExperience;
            _experience.Value = _playerLevel.CurrentExperience;
        }

        public void Dispose()
        {
            _playerLevel.OnExperienceChanged -= OnExperienceChanged;
            _playerLevel.OnLevelUp -= OnLevelUp;

            _subcription.Dispose();
        }
    }
}