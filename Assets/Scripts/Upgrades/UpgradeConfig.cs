using Game.Meta;
using UnityEngine;

public abstract class UpgradeConfig : ScriptableObject
{
    [field: SerializeField] public string Title { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: SerializeField, Space] public string Id { get; private set; }
    [field: SerializeField] public int MaxLevel { get; private set; } = 10;
    
    [field: SerializeField] public UpgradePriceTable PriceTable { get; private set; }

    public abstract Upgrade Create();

    public virtual void OnValidate()
    {
        PriceTable?.OnValidate(MaxLevel);
    }
}

