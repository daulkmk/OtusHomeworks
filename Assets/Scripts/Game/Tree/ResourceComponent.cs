using System;
using UnityEngine;

[Serializable]
public class ResourceComponent
{
    [field: SerializeField] public string Name { get; private set; }
}