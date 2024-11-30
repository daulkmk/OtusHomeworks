using Game.Meta;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgradeConfig", menuName = "Upgrades/Speed Config")]
public class SpeedUpgradeConfig : UpgradeConfig
{
    [field: SerializeField] public UpgradeTableFloat SpeedTable { get; private set; }

    public override Upgrade Create()
    {
        return new SpeedUpgrade(this);
    }

    public override void OnValidate()
    {
        base.OnValidate();

        SpeedTable?.OnValidate(MaxLevel);
    }
}
