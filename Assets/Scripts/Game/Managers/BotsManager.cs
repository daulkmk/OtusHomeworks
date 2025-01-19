using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BotsManager : IInitializable, ITickable
{
    private readonly Bot _botPrefab;
    private readonly List<Transform> _spawnPoints;
    private readonly List<Transform> _patrolPoints;
    private readonly IObjectResolver _objectResolver;
    private readonly Transform _botsContainer;
    private readonly List<Bot> _bots = new();

    public IReadOnlyList<Bot> Bots => _bots;

    public BotsManager(Bot botPrefab, Transform botsContainer, 
        List<Transform> spawnPoints, List<Transform> patrolPoints, 
        IObjectResolver objectResolver)
    {
        _botPrefab = botPrefab;
        _spawnPoints = spawnPoints;
        _patrolPoints = patrolPoints;
        _objectResolver = objectResolver;
        _botsContainer = botsContainer;
    }

    void IInitializable.Initialize()
    {
        SpawnBots();
    }
    private void SpawnBots()
    {
        //activeSelf condition for simple debugging
        foreach (var point in _spawnPoints.Where(p => p.gameObject.activeSelf))
        {
            SpawnBot(point);
        }
    }

    private void SpawnBot(Transform point)
    {
        var bot = _objectResolver.Instantiate(_botPrefab, point.position, Quaternion.identity, _botsContainer);
        _bots.Add(bot);

        bot.Initialize();
        bot.PatrolPointsComponent.SetPoints(_patrolPoints);
    }

    public void Tick()
    {
        _bots.ForEach(bot => bot.Tick());
    }
}
