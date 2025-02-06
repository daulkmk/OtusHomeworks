using Lessons.Lesson_Components.UI;
using Tutorial;
using UnityEngine;
using Zenject;

public class TutorialInstaller : MonoInstaller
{
    [SerializeField] private TutorialScreen _tutorialScreen;
    [SerializeField] private float _stayAliveStepDuration = 5;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TutorialScreen>()
            .FromInstance(_tutorialScreen)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<TutorialStepsFactory>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<HowToMoveAndShootStep>()
            .AsSingle();
        
        Container.BindInterfacesAndSelfTo<KillZombiesStep>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<StayAliveStep>()
            .AsSingle()
            .WithArguments(_stayAliveStepDuration);

        Container.BindInterfacesAndSelfTo<TutorialManager>()
            .AsSingle()
            .NonLazy();
    }
}