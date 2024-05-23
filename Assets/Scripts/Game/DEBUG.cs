using System.Collections.Generic;
using GameEngine;
using SaveLoad;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

//Use me for tests
public class DEBUG : MonoBehaviour
{
    [SerializeField] private List<Unit> _unitsToSpawn;

    private UnitManager _unitManager;
    private IGameRepository _gameRepository;
    private SaveLoadService _saveLoadSystem;
    private ResourceService _resourceService;

    [Inject]
    private void Construct(UnitManager unitManager, ResourceService resourceService, IGameRepository gameRepository, SaveLoadService saveLoadSystem)
    {
        _unitManager = unitManager;
        _gameRepository = gameRepository;
        _saveLoadSystem = saveLoadSystem;
        _resourceService = resourceService;
    }

    [Button]
    private void ClearData()
    {
        //To use when game is not running
        _gameRepository ??= new GameRepository(new AesCryptographyService());

        _gameRepository.Delete(UnitsSaveLoader.s_repositoryKey);
    }

    [Button]
    private void Save()
    {
        if (!Application.IsPlaying(gameObject))
            return;

        Debug.Log("SAVE");
        _saveLoadSystem.Save();
    }

    [Button]
    private void SpawnUnits()
    {
        if (!Application.IsPlaying(gameObject))
            return;

        foreach (var unit in _unitsToSpawn)
        {
            var spawnedUnit = _unitManager.SpawnUnit(unit, unit.Position, Quaternion.Euler(unit.Rotation));
            spawnedUnit.gameObject.SetActive(true);
        }
    }

    [Button]
    private void DestroyNextUnit()
    {
        if (!Application.IsPlaying(gameObject))
            return;

        var enumerator = _unitManager.GetAllUnits().GetEnumerator();
        if (enumerator.MoveNext())
            _unitManager.DestroyUnit(enumerator.Current);
        else
            Debug.Log("All units destroyed! Respawn them with button " + nameof(SpawnUnits));
    }

    [Button]
    private void RefillResources()
    {
        if (!Application.IsPlaying(gameObject))
            return;

        foreach (var resource in _resourceService.GetResources())
            resource.Amount = 5;

        Debug.Log("All resources refilled");
    }

    [Button]
    private void EmptyNextResource()
    {
        if (!Application.IsPlaying(gameObject))
            return;

        foreach (var resource in _resourceService.GetResources())
        {
            if (resource.Amount != 0)
            {
                resource.Amount = 0;
                Debug.Log("Resource emptied " + resource.name, resource.gameObject);
                return;
            }
        }

        Debug.Log("All resources are empty!");
    }
}
