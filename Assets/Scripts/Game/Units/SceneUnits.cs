using System.Collections.Generic;
using GameEngine;
using UnityEngine;

public class SceneUnits : MonoBehaviour
{
    [SerializeField] private List<Unit> _initialUnits;

    public IReadOnlyList<Unit> Prefabs => _initialUnits;
    public IReadOnlyList<UnitData> InitialUnits => _initialUnits.ConvertAll(x => new UnitData(x));
}
