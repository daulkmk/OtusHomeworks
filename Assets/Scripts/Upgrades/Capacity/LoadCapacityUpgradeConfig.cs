
using Game.Meta;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadCapacityUpgradeConfig", menuName = "Upgrades/Load Capacity Config")]
public class LoadCapacityUpgradeConfig : UpgradeConfig
{
    [field: SerializeField] public UpgradeTableInt CapacityTable { get; private set; }

    public override Upgrade Create()
    {
        return new LoadCapacityUpgrade(this);
    }

    public override void OnValidate()
    {
        base.OnValidate();

        CapacityTable.OnValidate(MaxLevel);
    }
}