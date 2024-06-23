using System.Collections.Generic;
using System.Linq;
using GameEngine;
using SaveLoad;
using UnityEngine;

public class ResourcesSaveLoader : SaveLoader<ResourceServiceData>
{
    private readonly ResourceService _resourceService;
    private readonly SceneResources _sceneResources;

    public ResourcesSaveLoader(ResourceService resourceService, SceneResources sceneResources, IGameRepository gameRepository)
        : base(gameRepository)
    {
        _resourceService = resourceService;
        _sceneResources = sceneResources;
    }

    protected override ResourceServiceData CreateData()
    {
        return new ResourceServiceData(_resourceService);
    }

    protected override void LoadWithoutData()
    {
        var resourceObjects = _sceneResources.Resources;
        _resourceService.SetResources(resourceObjects);
    }

    protected override void LoadWithData(ResourceServiceData data)
    {
        var resourceObjects = _sceneResources.Resources;
        ApplyDataToExistingResourses(resourceObjects, data);
        _resourceService.SetResources(resourceObjects);
    }

    private void ApplyDataToExistingResourses(IReadOnlyList<Resource> resourceObjects, ResourceServiceData data)
    {
        foreach (var resourceData in data.resources)
        {
            var resourceObject = resourceObjects.FirstOrDefault(x => x.ID == resourceData.id);
            if (resourceObject != null)
            {
                resourceObject.Amount = resourceData.amount;
                Debug.Log($"Load resource {resourceData.id} : {resourceData.amount}");
            }
            else
                Debug.Log("Cannot find resource object with id: " + resourceData.id);
        }
    }
}
