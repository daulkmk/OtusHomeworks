using System.Collections.Generic;
using GameEngine;
using UnityEngine;

public class SceneResources : MonoBehaviour
{
    [SerializeField] private List<Resource> _resources;

    public IReadOnlyList<Resource> Resources => _resources;
}
