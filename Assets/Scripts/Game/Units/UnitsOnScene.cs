using System.Collections.Generic;
using GameEngine;
using UnityEngine;

public class UnitsOnScene : MonoBehaviour
{
    [SerializeField] private List<Unit> _initialUnits;

    public IReadOnlyList<Unit> Prefabs => _initialUnits;
    public IReadOnlyList<UnitSnapshot> InitialUnits => _initialUnits.ConvertAll(x => new UnitSnapshot(x));
}
