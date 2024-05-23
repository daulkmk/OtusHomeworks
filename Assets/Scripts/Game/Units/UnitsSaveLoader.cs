using System.Collections.Generic;
using UnityEngine;
using SaveLoad;
using GameEngine;
using Cysharp.Threading.Tasks;

public class UnitsSaveLoader : ISaveLoader
{
    public const string s_repositoryKey = "UNITS";

    private readonly IGameRepository _repository;
    private readonly UnitManager _unitsManager;
    private readonly UnitPrefabsRegistry _prefabsRegistry;

    public UnitsSaveLoader(IGameRepository repository, UnitPrefabsRegistry prefabsRegistry, UnitManager unitManager)
    {
        _repository = repository;
        _unitsManager = unitManager;
        _prefabsRegistry = prefabsRegistry;
    }

    public async UniTask Load()
    {
        if (!await _repository.ContainsKey(s_repositoryKey))
            return;
            
        var unitsSnapshot = await _repository.Load<UnitManagerSnapshot>(s_repositoryKey);

        foreach (var snapshot in unitsSnapshot.units)
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

    public UniTask Save()
    {
        var unitsSnapshot = new UnitManagerSnapshot(_unitsManager);
        return _repository.Save(s_repositoryKey, unitsSnapshot);
    }

    [System.Serializable]
    private struct UnitManagerSnapshot
    {
        public List<UnitSnapshot> units;

        public UnitManagerSnapshot(UnitManager unitManager)
        {
            units = new();
            foreach (var unit in unitManager.GetAllUnits())
                units.Add(new UnitSnapshot(unit));
        }
    }

    [System.Serializable]
    private struct UnitSnapshot
    {
        public string type;
        public int hitPoints;
        public Vector3 position;
        public Vector3 rotation;

        public UnitSnapshot(Unit unit)
        {
            type = unit.Type;
            hitPoints = unit.HitPoints;
            position = unit.Position;
            rotation = unit.Rotation;
        }    
    }
}
