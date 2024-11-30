using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [SerializeField] private int _initialMoneyCount = 100;
    [SerializeField] private List<UpgradeConfig> _upgradeConfigs;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterGame(builder);
        RegisterUpgrades(builder);
        RegisterUI(builder);        
    }

    private void RegisterGame(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<MoneySystem>(Lifetime.Singleton)
            .WithParameter(_initialMoneyCount);

        builder.Register<ConverterStats>(Lifetime.Singleton);
        
        builder.Register<Converter>(Lifetime.Singleton)
            .As<Converter, ITickable>();

        builder.RegisterComponentInHierarchy<ConverterView>();
        builder.RegisterEntryPoint<ConverterViewController>();
    }

    private void RegisterUpgrades(IContainerBuilder builder)
    {
        builder.Register<UpgradesFactory>(Lifetime.Singleton)
            .WithParameter((IEnumerable<UpgradeConfig>)_upgradeConfigs);

        builder.RegisterEntryPoint<UpgradesSystem>();
    }

    private void RegisterUI(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<MoneyView>();
        builder.RegisterEntryPoint<MoneyViewController>();

        builder.Register<UpgradeViewControllerFactory>(Lifetime.Singleton);

        builder.RegisterComponentInHierarchy<UpgradesView>();
        builder.RegisterEntryPoint<UpgradesViewController>();

        builder.RegisterEntryPoint<NavigationController>();
    }
}
