

using System.Collections.Generic;
using GameEngine;

[System.Serializable]
public struct ResourceServiceData
{
    public List<ResourceData> resources;

    public ResourceServiceData(ResourceService resourceService)
    {
        resources = new List<ResourceData>();
        foreach (var resouce in resourceService.GetResources())
            resources.Add(new ResourceData(resouce));
    }
}

[System.Serializable]
public struct ResourceData
{
    public string id;
    public int amount;

    public ResourceData(Resource resource)
    {
        id = resource.ID;
        amount = resource.Amount;
    }
}