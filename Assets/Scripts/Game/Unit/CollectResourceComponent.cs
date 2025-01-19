using Entities;

public class CollectResourceComponent
{
    private readonly InventoryComponent _inventory;

    public CollectResourceComponent(InventoryComponent inventory)
    {
        _inventory = inventory;
    }

    public bool TryCollect(IEntity resourceEntity)
    {
        if (!resourceEntity.TryGet<ResourceComponent>(out var resource))
            return false;
        if (!resourceEntity.TryGet<LifeComponent>(out var life) || !life.IsAlive)
            return false;

        if (!_inventory.Add(resource.Name))
            return false;

        life.DieImmediatly();
        return true;
    }
}