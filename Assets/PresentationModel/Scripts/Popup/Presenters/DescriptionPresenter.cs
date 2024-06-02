using UniRx;

namespace ShootEmUp.PresentationModel
{
    public class DescriptionPresenter : IDescriptionPresenter
    {
        public IReadOnlyReactiveProperty<string> Name => _userInfoPresenter.Name;

        public IReadOnlyReactiveProperty<string> Description => _userInfoPresenter.Description;

        public IReadOnlyReactiveProperty<string> Level => _playerLevelPresenter.Level;

        public IReadOnlyReactiveProperty<UnityEngine.Sprite> Icon => _userInfoPresenter.Icon;

        private readonly UserInfoPresenter _userInfoPresenter;
        private readonly PlayerLevelPresenter _playerLevelPresenter;

        public DescriptionPresenter(UserInfoPresenter userInfoPresenter, PlayerLevelPresenter playerLevelPresenter)
        {
            _userInfoPresenter = userInfoPresenter;
            _playerLevelPresenter = playerLevelPresenter;
        }
    }
}