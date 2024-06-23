using UnityEngine;
using SaveLoad;
using GameEngine;
using System.Collections.Generic;

public class UnitsSaveLoader : SaveLoader<UnitManagerData>
{
    private readonly UnitManager _unitsManager;
    private readonly UnitPrefabsRegistry _prefabsRegistry;
    private readonly SceneUnits _sceneUnits;

    public UnitsSaveLoader(IGameRepository repository, SceneUnits sceneUnits, UnitPrefabsRegistry prefabsRegistry, UnitManager unitManager)
        : base(repository)
    {
        _unitsManager = unitManager;
        _prefabsRegistry = prefabsRegistry;
        _sceneUnits = sceneUnits;
    }

    protected override UnitManagerData CreateData()
    {
        return new UnitManagerData(_unitsManager);
    }

    protected override void LoadWithoutData()
    {
        LoadUnits(_sceneUnits.InitialUnits);
    }

    protected override void LoadWithData(UnitManagerData unitsData)
    {
        LoadUnits(unitsData.units);
    }

    private void LoadUnits(IReadOnlyCollection<UnitData> units)
    {
        foreach (var data in units)
        {
            var prefab = _prefabsRegistry.GetUnitPrefabByType(data.type);

            var unit = _unitsManager.SpawnUnit(
                prefab: prefab,
                position: data.position,
                rotation: Quaternion.Euler(data.rotation)
            );

            unit.HitPoints = data.hitPoints;

            Debug.Log($"Load unit {data.type} {data.hitPoints} {data.position} {data.rotation}");
        }
    }
}