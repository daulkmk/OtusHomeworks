using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Helpers
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private Entity _entity;

        private readonly HashSet<Entity> _collidedEntities = new();

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent(out Entity other))
                return;

            _collidedEntities.Add(other);

            if (_entity.HasData<Components.Collision>())
                return;

            _entity.AddData(new Components.Collision(other.Id));
            
            Debug.Log($"COLLISION {_entity.Id} {other.Id} {Time.frameCount}");
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent(out Entity other))
                return;

            _collidedEntities.Remove(other);

            if (!_entity.HasData<Components.Collision>())
                return;

            if (_collidedEntities.Count > 0)
            {
                ref var collisionComponent = ref _entity.GetData<Components.Collision>();
                collisionComponent.Entity = _collidedEntities.First().Id;
            }
            else
            {
                _entity.RemoveData<Components.Collision>();
                Debug.Log($"NO COLLISION {_entity.Id} {Time.frameCount}");
            }
        }
    }
}