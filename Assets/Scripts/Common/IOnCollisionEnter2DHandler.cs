using UnityEngine;

namespace ShootEmUp
{
    public interface IOnCollisionEnter2DHandler
    {
        void OnCollisionEnter2D(Collision2D collision);
    }
}