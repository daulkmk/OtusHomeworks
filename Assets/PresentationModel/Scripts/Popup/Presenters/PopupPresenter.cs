using Lessons.Architecture.PM;
using UniRx;

namespace ShootEmUp.PresentationModel
{
    public class PopupPresenter : IPopupPresenter
    {
        public IDescriptionPresenter DescriptionPresenter { get; private set; }
        public IStatsPresenter StatsPresenter { get; private set; }
        public IExpProgressBarPresenter ExpProgressBarPresenter { get; private set; }
        public ReactiveCommand LevelUpCommand => _playerLevelPresenter.LevelUpCommand;

        private readonly CharacterInfoPresenter _characterInfoPresenter;
        private readonly PlayerLevelPresenter _playerLevelPresenter;
        private readonly UserInfoPresenter _userInfoPresenter;
        
        private readonly CompositeDisposable _disposables = new();

        public PopupPresenter(CharacterInfo charecterInfo, PlayerLevel playerLevel, UserInfo userInfo)
        {
            _disposables.Add(_characterInfoPresenter = new CharacterInfoPresenter(charecterInfo));
            _disposables.Add(_playerLevelPresenter = new PlayerLevelPresenter(playerLevel));
            _disposables.Add(_userInfoPresenter = new UserInfoPresenter(userInfo));

            DescriptionPresenter = new DescriptionPresenter(_userInfoPresenter, _playerLevelPresenter);
            StatsPresenter = new StatsPresenter(_characterInfoPresenter);
            ExpProgressBarPresenter = new ExpProgressBarPresenter(_playerLevelPresenter);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}