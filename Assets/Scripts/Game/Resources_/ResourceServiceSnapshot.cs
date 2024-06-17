

using System.Collections.Generic;
using GameEngine;

[System.Serializable]
public struct ResourceServiceSnapshot
{
    public List<ResourceSnapsot> resources;

    public ResourceServiceSnapshot(ResourceService resourceService)
    {
        resources = new List<ResourceSnapsot>();
        foreach (var resouce in resourceService.GetResources())
            resources.Add(new ResourceSnapsot(resouce));
    }
}

[System.Serializable]
public struct ResourceSnapsot
{
    public string id;
    public int amount;

    public ResourceSnapsot(Resource resource)
    {
        id = resource.ID;
        amount = resource.Amount;
    }
}