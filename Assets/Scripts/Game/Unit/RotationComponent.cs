using System;
using UnityEngine;
using VContainer.Unity;

public class LookDirectionComponent : ITickable
{
    private readonly Transform _transform;

    [field: SerializeField] public Vector3 Direction { get; set; }
    [field: SerializeField] public bool CanRotate { get; set; } = true;

    public LookDirectionComponent(Transform transform)
    {
        _transform = transform;

        Direction = _transform.forward;
    }

    void ITickable.Tick()
    {
        if (CanRotate && Direction != Vector3.zero)
        {
            _transform.forward = Direction;
        }
    }
}
