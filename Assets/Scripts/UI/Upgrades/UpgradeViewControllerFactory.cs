using VContainer;

public class UpgradeViewControllerFactory
{
    private readonly IObjectResolver _objectResolver;

    public UpgradeViewControllerFactory(IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
    }

    public UpgradeViewController Create(UpgradeView view, Upgrade upgrade)
    {
        return new UpgradeViewController(view: view, 
            upgrade: upgrade, 
            upgradesSystem: _objectResolver.Resolve<IUpgradesSystem>(),
            moneySystem: _objectResolver.Resolve<IMoneySystem>()
        );
    }
}