using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Entities;
using UnityEngine;
using VContainer.Unity;

public class TreesManager : IInitializable, IDisposable
{
    private readonly Tree _treePrefab;
    private readonly List<Transform> _spawnPoints;
    private readonly List<Tree> _trees = new();
    private readonly float _respawnTreeDelay = 3f;

    private readonly CancellationTokenSource _cts = new();

    public IReadOnlyList<Tree> Trees => _trees;

    public TreesManager(Tree treePrefab, List<Transform> spawnPoints, float respawnTreeDelay)
    {
        _treePrefab = treePrefab;
        _spawnPoints = spawnPoints;
        _respawnTreeDelay = respawnTreeDelay;
    }

    void IInitializable.Initialize()
    {
        SpawnAllTrees();
    }

    void IDisposable.Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private void SpawnAllTrees()
    {
        //activeSelf condition for simple debugging
        foreach (var point in _spawnPoints.Where(p => p.gameObject.activeSelf))
        {
            if (point.childCount == 0)
            {
                SpawnTree(point);
            }
        }
    }

    private void SpawnTree(Transform point)
    {
        var tree = GameObject.Instantiate(_treePrefab, point);
        _trees.Add(tree);

        tree.Initialize();

        tree.LifeComponent.OnDeath += OnTreeDeath;
    }

    private async void OnTreeDeath(IEntity treeEntity)
    {
        var tree = treeEntity as Tree;

        tree.LifeComponent.OnDeath -= OnTreeDeath;
        _trees.Remove(tree);

        var spawnPoint = tree.transform.parent;

        await UniTask.WaitForSeconds(_respawnTreeDelay, cancellationToken: _cts.Token);

        SpawnTree(spawnPoint);
    }
}
