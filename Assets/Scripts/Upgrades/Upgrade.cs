using UnityEngine;

public abstract class Upgrade
{
    private readonly UpgradeConfig _config;

    public string Id => _config.Id;
    public string Title => _config.Title;
    public Sprite Icon => _config.Icon;
    public int MaxLevel => _config.MaxLevel;
    public int NextPrice => IsMaxLevel ? 0 : _config.PriceTable.GetPrice(Level + 1);
    public int Level { get; private set; } = 1;

    public bool IsMaxLevel => Level >= MaxLevel;

    public Upgrade(UpgradeConfig config)
    {
        _config = config;
    }

    public void SetLevel(int value)
    {
        Level = Mathf.Min(value, MaxLevel);
        OnLevelChanged();
    }

    public void LevelUp()
    {
        if (IsMaxLevel)
            throw new System.Exception("Cannot level up - max level reached! " + Id);

        Level++;
        OnLevelChanged();
    }

    protected abstract void OnLevelChanged();

    public abstract string GetStatValueFormatted();
}