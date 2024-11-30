
using Game.Meta;
using UnityEngine;

[CreateAssetMenu(fileName = "UnloadCapacityUpgradeConfig", menuName = "Upgrades/Unload Capacity Config")]
public class UnloadCapacityUpgradeConfig : UpgradeConfig
{
    [field: SerializeField] public UpgradeTableInt CapacityTable { get; private set; }

    public override Upgrade Create()
    {
        return new UnloadCapacityUpgrade(this);
    }

    public override void OnValidate()
    {
        base.OnValidate();

        CapacityTable.OnValidate(MaxLevel);
    }
}