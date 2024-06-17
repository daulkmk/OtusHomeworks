using UnityEngine;
using SaveLoad;
using GameEngine;
using System.Collections.Generic;

public class UnitsSaveLoader : AbstractSaveLoader<UnitManagerSnapshot>
{
    private readonly UnitManager _unitsManager;
    private readonly UnitPrefabsRegistry _prefabsRegistry;
    private readonly UnitsOnScene _unitsOnScene;

    public UnitsSaveLoader(IGameRepository repository, UnitsOnScene unitsOnScene, UnitPrefabsRegistry prefabsRegistry, UnitManager unitManager)
        : base(repository)
    {
        _unitsManager = unitManager;
        _prefabsRegistry = prefabsRegistry;
        _unitsOnScene = unitsOnScene;
    }

    protected override UnitManagerSnapshot CreateSnapshot()
    {
        return new UnitManagerSnapshot(_unitsManager);
    }

    protected override void LoadWithoutSnapshot()
    {
        LoadUnits(_unitsOnScene.InitialUnits);
    }

    protected override void LoadWithSnapshot(UnitManagerSnapshot unitsSnapshot)
    {
        LoadUnits(unitsSnapshot.units);
    }

    private void LoadUnits(IReadOnlyCollection<UnitSnapshot> units)
    {
        foreach (var snapshot in units)
        {
            var prefab = _prefabsRegistry.GetUnitPrefabByType(snapshot.type);

            var unit = _unitsManager.SpawnUnit(
                prefab: prefab,
                position: snapshot.position,
                rotation: Quaternion.Euler(snapshot.rotation)
            );

            unit.HitPoints = snapshot.hitPoints;

            Debug.Log($"Load unit {snapshot.type} {snapshot.hitPoints} {snapshot.position} {snapshot.rotation}");
        }
    }
}