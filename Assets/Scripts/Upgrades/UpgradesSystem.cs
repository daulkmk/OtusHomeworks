using System.Collections;
using System.Collections.Generic;
using VContainer.Unity;

public class UpgradesSystem : IUpgradesSystem, IInitializable
{
    private readonly IMoneySystem _moneySystem;
    private readonly Dictionary<string, Upgrade> _upgradeById = new();
    
    public UpgradesSystem(UpgradesFactory upgradesFactory, IMoneySystem moneySystem)
    {
        _moneySystem = moneySystem;

        var upgrades = upgradesFactory.Create();
        foreach (var upgrade in upgrades)
        {
            _upgradeById[upgrade.Id] = upgrade;
        }
    }

    void IInitializable.Initialize()
    {
        //Сохранения не реализованы - всегда начинаем с 1 уровня
        foreach (var upgrade in GetAllUpgrades())
        {
            upgrade.SetLevel(1);
        }
    }

    public IReadOnlyCollection<Upgrade> GetAllUpgrades() => _upgradeById.Values;

    public Upgrade GetUpgrade(string id)
    {
        return _upgradeById[id];
    }

    public bool CanLevelUp(Upgrade upgrade)
    {
        if (upgrade.IsMaxLevel)
            return false;

        return _moneySystem.CanSpendMoney(upgrade.NextPrice);
    }

    public void LevelUp(Upgrade upgrade)
    {
        if (!CanLevelUp(upgrade))
            throw new System.Exception("Cannot level up " + upgrade.Id);

        _moneySystem.SpendMoney(upgrade.NextPrice);
        upgrade.LevelUp();
    }
}
