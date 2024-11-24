
using System;
using System.Collections.Generic;
using System.Threading;
using VContainer.Unity;

public class ChestsViewController : IInitializable, IDisposable
{
    private readonly ChestsView _view;
    private readonly IChestViewControllerFactory _chestViewControllerFactory;
    private readonly CancellationTokenSource _cts = new();

    private readonly List<IDisposable> _disposables = new();
    private readonly List<IInitializable> _initializables = new();

    public ChestsViewController(ChestsView view, IChestViewControllerFactory chestViewControllerFactory)
    {
        _view = view;
        _chestViewControllerFactory = chestViewControllerFactory;
    }

    void IInitializable.Initialize()
    {
        CreateChestController(_view.ChestWooden, ChestTier.Wooden);
        CreateChestController(_view.ChestSteel, ChestTier.Steel);
        CreateChestController(_view.ChestGold, ChestTier.Gold);

        _initializables.ForEach(x => x.Initialize());
    }

    void IDisposable.Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();

        _disposables.ForEach(x => x.Dispose());
    }

    private void CreateChestController(ChestView view, ChestTier chestTier)
    {
        var controller = _chestViewControllerFactory.Create(view, chestTier);

        //Dynamically created objects must be handled manually
        _disposables.Add(controller);
        _initializables.Add(controller);
    }
}