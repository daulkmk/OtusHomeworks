using System.Collections.Generic;
using GameEngine;
using UnityEngine;

public class UnitPrefabsRegistry : MonoBehaviour
{
    [SerializeField] private List<Unit> _unitPrefabs = new();

    public int GetUnitId(string unitType)
    {
        return _unitPrefabs.FindIndex(x => x.Type == unitType);
    }

    public Unit GetUnitPrefabById(int id) => _unitPrefabs[id];

    public Unit GetUnitPrefabByType(string unitType)
    {
        var id = GetUnitId(unitType);
        return _unitPrefabs[id];
    }
}
