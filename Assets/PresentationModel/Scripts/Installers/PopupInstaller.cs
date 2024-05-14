using ShootEmUp.PresentationModel;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class PopupInstaller : MonoInstaller
    {
        [SerializeField] private PopupView _popupView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PopupView>()
                .FromInstance(_popupView)
                .AsSingle();

            Container.BindInterfacesTo<Popup>()
                .AsSingle();
        }
    }
}