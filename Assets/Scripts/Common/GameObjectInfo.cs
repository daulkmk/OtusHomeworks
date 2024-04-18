using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IGameObject
    {
        bool IsActiveInHierarchy { get; }
        int Layer { get; set; }

        ITransform Transform { get; }

        event Action OnEnable;
        event Action OnDisable;
    }

    public sealed class GameObjectInfo : IGameObject
    {
        bool IGameObject.IsActiveInHierarchy => _gameObject.activeInHierarchy;

        public ITransform Transform { get; private set; }
        public int Layer
        {
            get => _gameObject.layer;
            set => _gameObject.layer = value;
        }

        private readonly GameObject _gameObject;
        private OnEnableEvent _onEnable;
        private OnDisableEvent _onDissable;

        public event Action OnEnable
        {
            add
            {
                if (_onEnable == null)
                    _onEnable = GetOrAddComponent<OnEnableEvent>();
                _onEnable.OnEnabled += value;
            }

            remove
            {
                if (_onEnable != null)
                    _onEnable.OnEnabled -= value;
            }
        }

        public event Action OnDisable
        {
            add
            {
                if (_onDissable == null)
                    _onDissable = GetOrAddComponent<OnDisableEvent>();
                _onDissable.OnDisabled += value;
            }

            remove
            {
                if (_onDissable != null)
                    _onDissable.OnDisabled -= value;
            }
        }

        public GameObjectInfo(GameObject gameObject, ITransform transform)
        {
            _gameObject = gameObject;
            Transform = transform;
        }

        private T GetOrAddComponent<T>() where T : Component
        {
            if (_gameObject.TryGetComponent<T>(out var component))
                return component;
            return _gameObject.AddComponent<T>();
        }
    }
}