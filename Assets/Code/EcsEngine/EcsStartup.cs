using System;
using Client.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    internal sealed class EcsStartup : MonoBehaviour 
    {
        [SerializeField] private InitializeUnitsSystemFactory _initializeUnitsSystemFactory;
        [SerializeField] private SpawnRequestSystemFactory _spawnRequestSystemFactory;

        private EcsWorld _defaultWorld;
        private EcsWorld _eventsWorld;
        private IEcsSystems _systems;
        private EntityManager _entityManager;

        private void Awake()
        {
            _entityManager = new EntityManager();

            _defaultWorld = new EcsWorld();
            _eventsWorld = new EcsWorld();

            _systems = new EcsSystems(_defaultWorld);
            _systems.AddWorld(_eventsWorld, EcsWorlds.EVENTS);

            _systems
                .Add(_initializeUnitsSystemFactory.Create())

                .Add(new PathfindingSystem())

                .Add(new MovementSystem())
                .Add(new RotationSystem())

                .Add(new FindTargetSystem())

                .Add(new AimSystem())

                .Add(new FireRequestSystem())
                .Add(_spawnRequestSystemFactory.Create())

                .Add(new ProjectileDamageSystem())
                
                .Add(new DamageSystem())
                .Add(new DeathSystem())

                .Add(new TransformViewSystem())

#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS));
#endif
        }

        private void Start()
        {
            _entityManager.Initialize(_defaultWorld);
            _systems.Inject(_entityManager);
            _systems.Init();
        }

        private void Update() 
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null) 
            {
                _systems.Destroy();
                _systems = null;
            }
            
            if (_defaultWorld != null) 
            {
                _defaultWorld.Destroy();
                _defaultWorld = null;
            }

            if (_eventsWorld != null) 
            {
                _eventsWorld.Destroy();
                _eventsWorld = null;
            }
        }
    }
}