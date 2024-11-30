using VContainer;

public class SpeedUpgrade : Upgrade
{
    private readonly SpeedUpgradeConfig _config;
    private ConverterStats _converterStats;

    public SpeedUpgrade(SpeedUpgradeConfig config)
        : base(config)
    {
        _config = config;
    }

    [Inject]
    private void Construct(ConverterStats converterStats)
    {
        _converterStats = converterStats;
    }

    protected override void OnLevelChanged()
    {
        var speed = GetStatValue(Level);
        _converterStats.SetSpeed(speed);
    }

    private float GetStatValue(int level)
    {
        return _config.SpeedTable.GetValue(level);
    }

    public override string GetStatValueFormatted()
    {
        var speed = GetStatValue(Level);
        string value = Title + ": " + speed;

        if (!IsMaxLevel)
        {
            var delta = GetStatValue(Level + 1) - speed;
            string sign = delta > 0 ? "+" : "-";
            value += $" (<color=green>{sign}{delta.ToString("0.00")}</color>)";
        }

        return value;
    }
}
