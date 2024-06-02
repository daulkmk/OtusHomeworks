using UniRx;

namespace ShootEmUp.PresentationModel
{
    public class ExpProgressBarPresenter : IExpProgressBarPresenter
    {
        private const string s_prefix = "EXP: ";
        private readonly PlayerLevelPresenter _playerLevelPresenter;

        public IReadOnlyReactiveProperty<int> Experience => _playerLevelPresenter.Experience;

        public IReadOnlyReactiveProperty<int> RequiredExperience => _playerLevelPresenter.RequiredExperience;

        public ExpProgressBarPresenter(PlayerLevelPresenter playerLevelPresenter)
        {
            _playerLevelPresenter = playerLevelPresenter;
        }

        public string GetText(int progress, int maxValue)
        {
            return $"{s_prefix}{progress}/{maxValue}";
        }
    }
}