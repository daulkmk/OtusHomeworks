using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CollisionEnter2DProxy : MonoBehaviour
    {
        private IOnCollisionEnter2DHandler[] _handlers;

        [Inject]
        private void Construct(IOnCollisionEnter2DHandler[] handlers)
        {
            _handlers = handlers;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (var handler in _handlers)
                handler.OnCollisionEnter2D(collision);
        }
    }
}