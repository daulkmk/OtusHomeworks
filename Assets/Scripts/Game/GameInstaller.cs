using UnityEngine;
using GameEngine;
using SaveLoad;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Transform _unitsContainer;
    [SerializeField] private UnitPrefabsRegistry _unitPrefabsRegistry;
    [SerializeField] private SceneResources _resourcesOnScene;
    [SerializeField] private SceneUnits _unitsOnScene;

    public override void InstallBindings()
    {
        BindSaveLoadService();
        BindUnits();
        BindResources();
        BindGameControllers();
    }

    private void BindSaveLoadService()
    {
        Container.BindInterfacesTo<AesCryptographyService>()
            .AsSingle();

        Container.BindInterfacesTo<EncryptedSavesContainer>()
            .AsSingle()
            .WithArguments(new LocalFilesContainer());

        Container.BindInterfacesTo<GameRepository>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<SaveLoadService>()
            .AsSingle();
    }

    private void BindUnits()
    {
        var unitManager = new UnitManager(_unitsContainer);

        Container.BindInterfacesAndSelfTo<UnitManager>()
            //.WithArguments(_unitsContainer) ! Causes ZenjectException: Passed unnecessary parameters when injecting into type 'UnitManager'. 
            .FromInstance(unitManager) //Use instance instead (KISS solution)
            .AsSingle();
            
        Container.QueueForInject(unitManager);

        Container.BindInterfacesAndSelfTo<UnitPrefabsRegistry>()
            .FromInstance(_unitPrefabsRegistry)
            .AsSingle();

        Container.QueueForInject(_unitPrefabsRegistry);

        Container.BindInterfacesAndSelfTo<SceneUnits>()
            .FromInstance(_unitsOnScene)
            .AsSingle();

        Container.QueueForInject(_unitsOnScene);

        Container.BindInterfacesAndSelfTo<UnitsSaveLoader>()
            .AsSingle();
    }

    private void BindResources()
    {
        Container.BindInterfacesAndSelfTo<ResourceService>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<SceneResources>()
            .FromInstance(_resourcesOnScene)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<ResourcesSaveLoader>()
            .AsSingle();
    }

    private void BindGameControllers()
    {
        Container.BindInterfacesAndSelfTo<GameController>()
            .AsSingle()
            .NonLazy();
    }
}
