using System.Collections.Generic;
using GameEngine;
using UnityEngine;

[System.Serializable]
public struct UnitManagerData
{
    public List<UnitData> units;

    public UnitManagerData(UnitManager unitManager)
    {
        units = new();
        foreach (var unit in unitManager.GetAllUnits())
        {
            if (unit != null)
                units.Add(new UnitData(unit));
        }
    }
}

[System.Serializable]
public struct UnitData
{
    public string type;
    public int hitPoints;
    public Vector3 position;
    public Vector3 rotation;

    public UnitData(Unit unit)
    {
        type = unit.Type;
        hitPoints = unit.HitPoints;
        position = unit.Position;
        rotation = unit.Rotation;
    }
}