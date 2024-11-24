using UnityEngine;

/// <summary>
/// Chest are predefined - so they are hardcoded! KISS
/// </summary>
public class ChestsView : MonoBehaviour
{
    [field: SerializeField]
    public ChestView ChestWooden { get; private set; }

    [field: SerializeField]
    public ChestView ChestSteel { get; private set; }

    [field: SerializeField]
    public ChestView ChestGold { get; private set; }
}
