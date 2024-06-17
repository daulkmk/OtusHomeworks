using Atomic.Elements;
using Atomic.Objects;
using Lessons.Lesson_AtomicIntroduction;
using UnityEngine;
using Zenject;

public class ZombieController : ITickable, IInitializable
{
    private readonly IAtomicEntity _zombie;
    private readonly IAtomicEntity _player;
    private readonly float _targetReachedDistance;

    private MoveToTargetMechanics _moveToTargetMechanics;
    private GetZombiePunchActionRequest _getZombiePunchActionRequest;

    public ZombieController(IAtomicEntity zombie, 
        [Inject(Id = ObjectType.Player)] IAtomicEntity player,
        float targetReachedDistance
    )
    {
        _zombie = zombie;
        _player = player;
        _targetReachedDistance = targetReachedDistance;
    }

    void IInitializable.Initialize()
    {
        InitializeMove();
        InitializeAim();
        InitializePunch();
    }

    private void InitializeMove()
    {
        var moveDirection = _zombie.Get<IAtomicVariableObservable<Vector3>>(MoveAPI.MoveDirection);
        var myPosition = _zombie.Get<IAtomicValueObservable<Vector3>>(CommonAPI.Position);
        var targetPosition = _player.Get<IAtomicValueObservable<Vector3>>(CommonAPI.Position);

        _moveToTargetMechanics = new MoveToTargetMechanics(
            moveDirection: moveDirection,
            targetPosition: targetPosition,
            myPosition: myPosition,
            minDistance: _targetReachedDistance
        );

        var playerIsDead = _player.Get<IAtomicValueObservable<bool>>(LifeAPI.IsDead);
        _moveToTargetMechanics.AppendCondition(() => !playerIsDead.Value);
    }

    private void InitializeAim()
    {
        var playerPosition = _player.Get<IAtomicValueObservable<Vector3>>(CommonAPI.Position);
        var lookTarget = _zombie.Get<IAtomicVariableObservable<Vector3>>(MoveAPI.LookTarget);

        lookTarget.Value = playerPosition.Value;
        playerPosition.Subscribe(input => lookTarget.Value = input);
    }

    private void InitializePunch()
    {
        var punchRequest = _zombie.Get<IAtomicAction>(PunchAPI.PunchRequest);
        var isTargetInRange = _zombie.Get<IAtomicValueObservable<bool>>(PunchAPI.IsTargetInRange);
        var isTargetDead = _player.Get<IAtomicVariableObservable<bool>>(LifeAPI.IsDead);
        
        _getZombiePunchActionRequest = new GetZombiePunchActionRequest(isTargetInRange: isTargetInRange
            , isTargetDead: isTargetDead
        );
        _getZombiePunchActionRequest.OnPunchRequested.Subscribe(punchRequest.Invoke);
    }

    void ITickable.Tick()
    {
        _moveToTargetMechanics.Update();
        _getZombiePunchActionRequest.Update();
    }
}
