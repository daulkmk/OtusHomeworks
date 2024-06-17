using System.Collections.Generic;
using GameEngine;
using UnityEngine;

[System.Serializable]
public struct UnitManagerSnapshot
{
    public List<UnitSnapshot> units;

    public UnitManagerSnapshot(UnitManager unitManager)
    {
        units = new();
        foreach (var unit in unitManager.GetAllUnits())
        {
            if (unit != null)
                units.Add(new UnitSnapshot(unit));
        }
    }
}

[System.Serializable]
public struct UnitSnapshot
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