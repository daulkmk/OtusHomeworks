using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using SaveLoad;
using Zenject;

public class GameController : IInitializable
{
    private readonly SaveLoadService _saveLoadSystem;

    public GameController(SaveLoadService saveLoadSystem)
    {
        _saveLoadSystem = saveLoadSystem;
    }

    void IInitializable.Initialize()
    {
        Debug.Log("LOAD");

        _saveLoadSystem.Load().Forget();
    }
}
