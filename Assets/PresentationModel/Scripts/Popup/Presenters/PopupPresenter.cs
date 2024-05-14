using Lessons.Architecture.PM;
using UniRx;

namespace ShootEmUp.PresentationModel
{
    public class PopupPresenter : IPopupPresenter
    {
        public IReadOnlyReactiveProperty<string> Name => _userInfoPresenter.Name;

        public IReadOnlyReactiveProperty<string> Description => _userInfoPresenter.Description;

        public IReadOnlyReactiveProperty<string> Level => _playerLevelPresenter.Level;

        public IReadOnlyReactiveProperty<UnityEngine.Sprite> Icon => _userInfoPresenter.Icon;

        public IReadOnlyReactiveProperty<int> Experience => _playerLevelPresenter.Experience;

        public IReadOnlyReactiveProperty<int> RequiredExperience => _playerLevelPresenter.RequiredExperience;

        public IReadOnlyReactiveCollection<IStatPresenter> Stats => _characterInfoPresenter.Stats;

        public ReactiveCommand LevelUpCommand => _playerLevelPresenter.LevelUpCommand;

        private readonly CharacterInfoPresenter _characterInfoPresenter;
        private readonly PlayerLevelPresenter _playerLevelPresenter;
        private readonly UserInfoPresenter _userInfoPresenter;

        private readonly CompositeDisposable _disposables = new();

        public PopupPresenter(CharacterInfo charecterInfo, PlayerLevel playerLevel, UserInfo userInfo)
        {
            //TODO: Presenters factory
            _disposables.Add(_characterInfoPresenter = new CharacterInfoPresenter(charecterInfo));
            _disposables.Add(_playerLevelPresenter = new PlayerLevelPresenter(playerLevel));
            _disposables.Add(_userInfoPresenter = new UserInfoPresenter(userInfo));
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}