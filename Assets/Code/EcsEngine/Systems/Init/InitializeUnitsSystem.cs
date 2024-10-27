using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client
{
    public sealed class InitializeUnitsSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<SpawnRequest> _spawnPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<EntityPrefab> _prefabPool = EcsWorlds.EVENTS;

        private readonly Entity _unitPrefabTeam1;
        private readonly Entity _unitPrefabTeam2;
        private readonly int _teamSize;
        private readonly float _offset;
        private readonly int _lineLength;

        public InitializeUnitsSystem(Entity unitPrefabTeam1, Entity unitPrefabTeam2, int teamSize, float offset, int lineLength)
        {
            _unitPrefabTeam1 = unitPrefabTeam1;
            _unitPrefabTeam2 = unitPrefabTeam2;
            _teamSize = teamSize;
            _offset = offset;
            _lineLength = lineLength;
        }

        public void Init(IEcsSystems systems)
        {
            float armyOffset = _offset * 2;

            for (int i = 0; i < _teamSize; i++)
            {
                float random = Random.Range(-_offset / 2, _offset / 2);

                int line = i / _lineLength;
                int indexInLine = i % _lineLength;

                var position = new Vector3(
                    x: armyOffset + random + line * _offset,
                    y: 0,
                    z: indexInLine * _offset + random
                );
                var rotation = Quaternion.LookRotation(Vector3.left);

                RequestSpawnUnit(_unitPrefabTeam1, position, rotation);

                position.x *= -1;
                rotation = Quaternion.LookRotation(Vector3.right);

                RequestSpawnUnit(_unitPrefabTeam2, position, rotation);
            }
        }

        private void RequestSpawnUnit(Entity prefab, Vector3 position, Quaternion rotation)
        {
            int spawnEvent = _eventWorld.Value.NewEntity();

            _spawnPool.Value.Add(spawnEvent) = new SpawnRequest();
            _positionPool.Value.Add(spawnEvent) = new Position { Value = position };
            _rotationPool.Value.Add(spawnEvent) = new Rotation { Value = rotation };
            _prefabPool.Value.Add(spawnEvent) = new EntityPrefab { Value = prefab };
        }
    }
}