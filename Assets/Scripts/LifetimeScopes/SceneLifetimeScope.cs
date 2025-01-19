using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{
    [Header(nameof(TreesManager))]
    [SerializeField] private Tree _treePrefab;
    [SerializeField] private List<Transform> _treesSpawnPoints;
    [SerializeField] private float _respawnTreeDelay = 5;

    [Header(nameof(BotsManager))]
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Transform _botsContainer;
    [SerializeField] private List<Transform> _botSpawnPoints;
    [SerializeField] private List<Transform> _botPatrolPoints;


    protected override void Configure(IContainerBuilder builder)
    {
        RegisterConverter(builder);

        builder.RegisterEntryPoint<TreesManager>()
            .As<TreesManager>()
            .WithParameter(_treePrefab)
            .WithParameter(_treesSpawnPoints)
            .WithParameter(_respawnTreeDelay);

        builder.RegisterEntryPoint<BotsManager>()
            .As<BotsManager>()
            .WithParameter(_botPrefab)
            .WithParameter(_botsContainer)
            .WithParameter("spawnPoints",_botSpawnPoints)
            .WithParameter("patrolPoints",_botPatrolPoints);

        builder.Register<BotsHiveMindData>(Lifetime.Singleton);
        builder.Register<BotBlackboardFactory>(Lifetime.Singleton);
    }
    
    private void RegisterConverter(IContainerBuilder builder)
    {
        builder.Register<ConverterStats>(Lifetime.Singleton);
        
        builder.Register<Converter>(Lifetime.Singleton)
            .As<Converter, ITickable>();

        builder.RegisterComponentInHierarchy<ConverterView>();
        builder.RegisterEntryPoint<ConverterViewController>();

        builder.RegisterComponentInHierarchy<ConverterDock>();
    }
}
