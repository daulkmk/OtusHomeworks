using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : IOnCollisionEnter2DHandler, IPauseGameListener
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly IGameObject _gameObject;

        public event Action<Bullet> OnDeath;

        public bool IsPlayer { get; set; }
        public int Damage { get; set; }
        public Vector3 Position => _gameObject.Transform.Position;

        public Bullet(bool isPlayer, int damage, Rigidbody2D rigidbody2D, SpriteRenderer spriteRenderer, IGameObject gameObject)
        {
            _rigidbody2D = rigidbody2D;
            _spriteRenderer = spriteRenderer;
            _gameObject = gameObject;

            IsPlayer = isPlayer;
            Damage = damage;
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            _gameObject.Layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            _gameObject.Transform.Position = position;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        void IOnCollisionEnter2DHandler.OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision.gameObject);
            OnDeath?.Invoke(this);
        }

        private void DealDamage(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IDamagable>(out var damagable))
                damagable.ApplyDamage(Damage, IsPlayer);
        }

        void IPauseGameListener.OnGamePaused(bool paused)
        {
            _rigidbody2D.simulated = !paused;
        }
    }
}