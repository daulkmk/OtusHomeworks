using Zenject;

namespace ShootEmUp
{
    public class InputWeaponInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputWeaponController>().AsSingle();
        }
    }
}