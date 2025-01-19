
using System;
using UnityEngine;
using VContainer.Unity;

public class MoveToTargetComponent : ITickable
{
    private readonly MoveComponent _moveComponent;
    private readonly LookDirectionComponent _lookDirectionComponent;
    private readonly float _targetReachedDistanceSQR;

    public Vector3 Target { get; set; }

    public MoveToTargetComponent(MoveComponent moveComponent, 
        LookDirectionComponent lookDirectionComponent,
        float targetReachedDistance
        )
    {
        _moveComponent = moveComponent;
        _lookDirectionComponent = lookDirectionComponent;
        _targetReachedDistanceSQR = targetReachedDistance * targetReachedDistance;

        Target = _moveComponent.Position;
    }

    void ITickable.Tick()
    {
        var direction = Target - _moveComponent.Position;

        _lookDirectionComponent.Direction = direction;

        if (direction.sqrMagnitude <= _targetReachedDistanceSQR)
            direction = Vector3.zero;

        _moveComponent.Direction = direction;
    }

    public bool IsTargetReached()
    {
        var direction = Target - _moveComponent.Position;
        return direction.sqrMagnitude <= _targetReachedDistanceSQR;
    }
}