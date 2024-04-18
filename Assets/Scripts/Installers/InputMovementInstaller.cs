using Zenject;

namespace ShootEmUp
{
    public class InputMovementInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputMovementController>().AsSingle();
        }
    }
}