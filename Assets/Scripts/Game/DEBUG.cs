using System.Collections.Generic;
using GameEngine;
using SaveLoad;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

//Use me for tests
public class DEBUG : MonoBehaviour
{
    private UnitManager _unitManager;
    private IGameRepository _gameRepository;
    private SaveLoadService _saveLoadSystem;
    private ResourceService _resourceService;
    private SceneUnits _sceneUnits;

    [Inject]
    private void Construct(UnitManager unitManager, SceneUnits unitsOnScene, ResourceService resourceService, IGameRepository gameRepository, SaveLoadService saveLoadSystem)
    {
        _unitManager = unitManager;
        _gameRepository = gameRepository;
        _saveLoadSystem = saveLoadSystem;
        _resourceService = resourceService;
        _sceneUnits = unitsOnScene;
    }

    [Button]
    private void ClearData()
    {
        //To use when game is not running
        _gameRepository ??= new GameRepository(new LocalFilesContainer());

        _gameRepository.DeleteAll();
    }

    [Button]
    private void Save()
    {
        ValidatePlaymode();

        Debug.Log("SAVE");
        _saveLoadSystem.Save();
    }

    [Button]
    private void Load()
    {
        ValidatePlaymode();

        DestroyAllUnits();

        _saveLoadSystem.Load();
    }
    
    [PropertySpace]
    [Button]
    private void DestroyActiveUnitsAndSpawnNew()
    {
        ValidatePlaymode();

        DestroyAllUnits();

        foreach (var unit in _sceneUnits.Prefabs)
        {
            var spawnedUnit = _unitManager.SpawnUnit(unit, unit.Position, Quaternion.Euler(unit.Rotation));
            spawnedUnit.gameObject.SetActive(true);
        }
    }

    [Button]
    private void Spawn(Unit unit)
    {
        ValidatePlaymode();

        var newUnit = _unitManager.SpawnUnit(unit, unit.Position, Quaternion.Euler(unit.Rotation));
        newUnit.gameObject.SetActive(true);

        UnityEditor.Selection.activeGameObject = newUnit.gameObject;
    }

    [Button]
    private void DestroyNextUnit()
    {
        ValidatePlaymode();

        var enumerator = _unitManager.GetAllUnits().GetEnumerator();
        if (enumerator.MoveNext())
            _unitManager.DestroyUnit(enumerator.Current);
        else
            Debug.Log("All units destroyed! Respawn them with a button " + nameof(DestroyActiveUnitsAndSpawnNew));
    }

    [PropertySpace]
    [Button]
    private void RefillResources()
    {
        ValidatePlaymode();

        foreach (var resource in _resourceService.GetResources())
            resource.Amount = 5;

        Debug.Log("All resources refilled");
    }

    [Button]
    private void EmptyNextResource()
    {
        ValidatePlaymode();

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

    private void ValidatePlaymode()
    {
        if (!Application.IsPlaying(gameObject))
            throw new System.Exception("Works only in play mode!");
    }

    private void DestroyAllUnits()
    {
        var units = new List<Unit>(_unitManager.GetAllUnits());
        foreach (var unit in units)
        {
            if (unit != null)
                _unitManager.DestroyUnit(unit);
        }
    }
}
