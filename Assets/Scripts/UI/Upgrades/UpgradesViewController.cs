using System;
using System.Collections.Generic;
using VContainer.Unity;

public class UpgradesViewController : IUpgradesViewController, IInitializable, IDisposable
{
    private readonly UpgradesView _view;
    private readonly IUpgradesSystem _upgradesSystem;
    private readonly UpgradeViewControllerFactory _ugradesViewControllerFactory;

    private readonly List<IDisposable> _disposables = new();
    private readonly List<IInitializable> _initializables = new();

    public UpgradesViewController(UpgradesView view, IUpgradesSystem upgradesSystem, UpgradeViewControllerFactory upgradesViewControllerFactory)
    {
        _view = view;
        _upgradesSystem = upgradesSystem;
        _ugradesViewControllerFactory = upgradesViewControllerFactory;
    }

    void IInitializable.Initialize()
    {
        _view.OnCloseClicked += OnCloseClicked;
        CreateUpgradeControllers();
        
        _initializables.ForEach(x => x.Initialize());
    }

    private void OnCloseClicked()
    {
        SetActive(false);
    }

    private void CreateUpgradeControllers()
    {
        var upgrades = _upgradesSystem.GetAllUpgrades();
        foreach (var upgrade in upgrades)
        {
            var upgradeView = _view.CreateUpgradeView();
            var upgradeViewController = _ugradesViewControllerFactory.Create(upgradeView, upgrade);

            //Нужно вручную обрабатывать динамически созданные объекты
            _disposables.Add(upgradeViewController);
            _initializables.Add(upgradeViewController);
        }
    }

    void IDisposable.Dispose()
    {
        _view.OnCloseClicked -= OnCloseClicked;

        _initializables.Clear();

        _disposables.ForEach(x => x.Dispose());
        _disposables.Clear();
    }

    public void SetActive(bool active)
    {
        _view.gameObject.SetActive(active);
    }
}
