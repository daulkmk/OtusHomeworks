using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameEngine;
using SaveLoad;
using UnityEngine;

public class ResourcesSaveLoad : ISaveLoader
{
    public const string s_repositoryKey = "RESOURCES";

    private readonly ResourceService _resourceService;
    private readonly ResourcesOnScene _resourcesOnScene;
    private readonly IGameRepository _gameRepository;

    public ResourcesSaveLoad(ResourceService resourceService, ResourcesOnScene resourcesOnScene, IGameRepository gameRepository)
    {
        _resourceService = resourceService;
        _resourcesOnScene = resourcesOnScene;
        _gameRepository = gameRepository;
    }


    public async UniTask Load()
    {
        var resourceObjects = _resourcesOnScene.Resources;

        if (await _gameRepository.ContainsKey(s_repositoryKey))
        {
            var snapshot = await _gameRepository.Load<ResourceServiceSnapshot>(s_repositoryKey);
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

        _resourceService.SetResources(resourceObjects);
    }

    public UniTask Save()
    {
        var snaphost = new ResourceServiceSnapshot(_resourceService);
        return _gameRepository.Save(s_repositoryKey, snaphost);
    }

    [System.Serializable]
    private struct ResourceServiceSnapshot
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
    private struct ResourceSnapsot
    {
        public string id;
        public int amount;

        public ResourceSnapsot(Resource resource)
        {
            id = resource.ID;
            amount = resource.Amount;
        }
    }
}
