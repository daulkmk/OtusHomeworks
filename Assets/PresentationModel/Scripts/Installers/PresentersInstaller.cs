using System.Collections;
using System.Collections.Generic;
using Lessons.Architecture.PM;
using Zenject;

namespace ShootEmUp.PresentationModel
{
    public class PresentersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<CharacterInfo, PlayerLevel, UserInfo, IPopupPresenter, PopupPresetnerFactory>()
                .To<PopupPresenter>();

            Container.BindFactory<CharacterStat, IStatPresenter, StatPresenterFactory>()
                .To<StatPresenter>();

            Container.BindFactory<PlayerLevel, PlayerLevelPresenter, PlayerLevelPresenterFactory>();
            Container.BindFactory<UserInfo, UserInfoPresenter, UserInfoPresenterFactory>();
        }
    }

    public class PopupPresetnerFactory : PlaceholderFactory<CharacterInfo, PlayerLevel, UserInfo, IPopupPresenter> { }
    public class StatPresenterFactory : PlaceholderFactory<CharacterStat, IStatPresenter> { }
    public class PlayerLevelPresenterFactory : PlaceholderFactory<PlayerLevel, PlayerLevelPresenter> { }
    public class UserInfoPresenterFactory : PlaceholderFactory<UserInfo, UserInfoPresenter> { }
}