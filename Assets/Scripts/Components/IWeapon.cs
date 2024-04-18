using UnityEngine;
using R3;

namespace ShootEmUp
{
    public interface IWeapon
    {
        Vector2 Position { get; }
        ReactiveProperty<bool> CanFire { get; }
        void Fire(Vector2 target);
    }
}