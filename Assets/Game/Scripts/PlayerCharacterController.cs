using System;
using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_AtomicIntroduction;
using UnityEngine;
using Zenject;

public class PlayerCharacterController : ITickable, IInitializable
{
    private readonly IAtomicEntity _caracter;

    private GetInputTargetMechanics _getInputTargetMechanics;
    private GetPlayerInputDirectionMechanic _getKeyboardInputDirectionMechanic;
    private GetPlayerShootActionRequest _getPlayerShootActionRequest;

    public event Action OnShootRequested;
    public event Action<Vector3> OnMoveDirectionChanged;

    public PlayerCharacterController([Inject(Id = ObjectType.Player)] IAtomicEntity player)
    {
        _caracter = player;
    }

    void IInitializable.Initialize()
    {
        InitializeMove();
        InitializeAim();
        InitializeShoot();
    }

    private void InitializeMove()
    {
        var moveDirection = _caracter.Get<IAtomicVariableObservable<Vector3>>(MoveAPI.MoveDirection);

        _getKeyboardInputDirectionMechanic = new GetPlayerInputDirectionMechanic();
        _getKeyboardInputDirectionMechanic.Direction.Subscribe(input =>
        {
            var direction = new Vector3(input.x, 0, input.y); ;

            moveDirection.Value = direction;
            OnMoveDirectionChanged?.Invoke(direction);
        });
    }

    private void InitializeAim()
    {
        var firePoint = _caracter.Get<IAtomicValueObservable<Vector3>>(ShootAPI.FirePointPosition);
        var getFirePointY = new AtomicFunction<float>(() => firePoint.Value.y);

        _getInputTargetMechanics =
            new GetInputTargetMechanics(camera: Camera.main, yPosition: getFirePointY);

        var lookTarget = _caracter.Get<IAtomicVariableObservable<Vector3>>(MoveAPI.LookTarget);

        _getInputTargetMechanics.Target.Subscribe(input => lookTarget.Value = input);
    }

    private void InitializeShoot()
    {
        var shootRequest = _caracter.Get<IAtomicAction>(ShootAPI.ShootRequest);

        _getPlayerShootActionRequest = new GetPlayerShootActionRequest();
        _getPlayerShootActionRequest.ShootRequested.Subscribe(() =>
        {
            shootRequest.Invoke();
            OnShootRequested?.Invoke();
        });
    }

    void ITickable.Tick()
    {
        _getKeyboardInputDirectionMechanic.Update();
        _getInputTargetMechanics.Update();
        _getPlayerShootActionRequest.Update();
    }
}
