using VContainer;

public class ChestViewControllerFactory : IChestViewControllerFactory
{
    private readonly IObjectResolver _objectResolver;

    public ChestViewControllerFactory(IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
    }

    public ChestViewController Create(ChestView view, ChestTier chestTier)
    {
        return new ChestViewController(
            view: view,
            chestTier: chestTier,
            rewardsController: _objectResolver.Resolve<IRewardsController>(),
            chestsDataManager: _objectResolver.Resolve<IChestsDataManager>()
        );
    }
}