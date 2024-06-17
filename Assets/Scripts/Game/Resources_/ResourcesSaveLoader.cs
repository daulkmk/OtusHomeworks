using System.Collections.Generic;
using System.Linq;
using GameEngine;
using SaveLoad;
using UnityEngine;

public class ResourcesSaveLoader : AbstractSaveLoader<ResourceServiceSnapshot>
{
    private readonly ResourceService _resourceService;
    private readonly ResourcesOnScene _resourcesOnScene;

    public ResourcesSaveLoader(ResourceService resourceService, ResourcesOnScene resourcesOnScene, IGameRepository gameRepository)
        : base(gameRepository)
    {
        _resourceService = resourceService;
        _resourcesOnScene = resourcesOnScene;
    }

    protected override ResourceServiceSnapshot CreateSnapshot()
    {
        return new ResourceServiceSnapshot(_resourceService);
    }

    protected override void LoadWithoutSnapshot()
    {
        var resourceObjects = _resourcesOnScene.Resources;
        _resourceService.SetResources(resourceObjects);
    }

    protected override void LoadWithSnapshot(ResourceServiceSnapshot snapshot)
    {
        var resourceObjects = _resourcesOnScene.Resources;
        ApplySnapshotToExistingResourses(resourceObjects, snapshot);
        _resourceService.SetResources(resourceObjects);
    }

    private void ApplySnapshotToExistingResourses(IReadOnlyList<Resource> resourceObjects, ResourceServiceSnapshot snapshot)
    {
        foreach (var resourceSnapshot in snapshot.resources)
        {
            var resourceObject = resourceObjects.FirstOrDefault(x => x.ID == resourceSnapshot.id);
            if (resourceObject != null)
            {
                resourceObject.Amount = resourceSnapshot.amount;
                Debug.Log($"Load resource {resourceSnapshot.id} : {resourceSnapshot.amount}");
            }
            else
                Debug.Log("Cannot find resource object with id: " + resourceSnapshot.id);
        }
    }
}
