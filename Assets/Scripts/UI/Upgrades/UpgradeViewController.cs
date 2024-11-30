using System;
using VContainer.Unity;

public class UpgradeViewController : IInitializable, IDisposable
{
    private readonly UpgradeView _view;
    private readonly Upgrade _upgrade;
    private readonly IUpgradesSystem _upgradesSystem;
    private readonly IMoneySystem _moneySystem;

    public UpgradeViewController(UpgradeView view, Upgrade upgrade, IUpgradesSystem upgradesSystem, IMoneySystem moneySystem)
    {
        _view = view;
        _upgrade = upgrade;
        _upgradesSystem = upgradesSystem;
        _moneySystem = moneySystem;
    }

    void IInitializable.Initialize()
    {
        _moneySystem.OnMoneyChanged += OnMoneyChanged;
        _view.OnUpgradeClick += OnUpgradeClick;

        _view.SetTitle(_upgrade.Title);
        _view.SetIcon(_upgrade.Icon);

        UpdateView();
    }

    void IDisposable.Dispose()
    {
        _moneySystem.OnMoneyChanged -= OnMoneyChanged;
        _view.OnUpgradeClick -= OnUpgradeClick;
    }

    private void OnMoneyChanged() => UpdateView();

    private void UpdateView()
    {
        _view.SetIsMaxed(_upgrade.IsMaxLevel);
        _view.SetCanLevelUp(_upgradesSystem.CanLevelUp(_upgrade));
        _view.SetLevel($"Level: {_upgrade.Level}/{_upgrade.MaxLevel}");
        _view.SetPrice(_upgrade.NextPrice.ToString());
        _view.SetValue(_upgrade.GetStatValueFormatted());
    }

    private void OnUpgradeClick()
    {
        if (_upgradesSystem.CanLevelUp(_upgrade))
        {
            _upgradesSystem.LevelUp(_upgrade);
        }

        UpdateView();
    }
}
