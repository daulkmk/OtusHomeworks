using VContainer;

public class LoadCapacityUpgrade : Upgrade
{
    private readonly LoadCapacityUpgradeConfig _config;
    private ConverterStats _converterStats;

    public LoadCapacityUpgrade(LoadCapacityUpgradeConfig config) 
        : base(config)
    {
        _config = config;
    }

    [Inject]
    private void Construct(ConverterStats converterStats)
    {
        _converterStats = converterStats;
    }

    private int GetStatValue(int level)
    {
        return _config.CapacityTable.GetValue(level);
    }

    protected override void OnLevelChanged()
    {
        var loadCapacity = _config.CapacityTable.GetValue(Level);
        _converterStats.SetLoadCapacity(loadCapacity);
    }

    public override string GetStatValueFormatted()
    {
        var speed = GetStatValue(Level);
        string value = Title + ": " + speed;

        if (!IsMaxLevel)
        {
            var delta = GetStatValue(Level + 1) - speed;
            string sign = delta > 0 ? "+" : "-";
            value += $" (<color=green>{sign}{delta}</color>)";
        }

        return value;
    }
}
