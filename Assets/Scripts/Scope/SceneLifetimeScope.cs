using UnityEngine.Networking;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        UnityWebRequest.ClearCookieCache();

        RegisterServices(builder);
        RegisterUI(builder);
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SeverTime>();
        builder.RegisterEntryPoint<GameSessionsManager>();

        builder.RegisterEntryPoint<RewardsController>();

        builder.RegisterEntryPoint<ChestDataFactory>();
        builder.RegisterEntryPoint<ChestsDataManager>();
    }

    private void RegisterUI(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<SessionsView>();
        builder.RegisterComponentInHierarchy<ChestsView>();

        builder.RegisterEntryPoint<ChestViewControllerFactory>();

        builder.RegisterEntryPoint<SessionsViewController>();
        builder.RegisterEntryPoint<ChestsViewController>();   
    }
}
