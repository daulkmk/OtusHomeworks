using System.Numerics;
using Atomic.Elements;
using Atomic.Extensions;

public class CommonAPI
{
    [Contract(typeof(IAtomicValueObservable<Vector3>))]
    public const string Position = nameof(Position);
}

public class MoveAPI
{
    [Contract(typeof(IAtomicVariableObservable<Vector3>))]
    public const string MoveDirection = nameof(MoveDirection);

    [Contract(typeof(IAtomicVariableObservable<Vector3>))]
    public const string LookTarget = nameof(LookTarget);
}

public class ShootAPI
{
    [Contract(typeof(IAtomicAction))]
    public const string ShootRequest = nameof(ShootRequest);

    [Contract(typeof(IAtomicValueObservable<Vector3>))]
    public const string FirePointPosition = nameof(FirePointPosition);

    [Contract(typeof(IAtomicVariableObservable<int>))]
    public const string Ammo = nameof(Ammo);
}

public class PunchAPI
{
    [Contract(typeof(IAtomicAction))]
    public const string PunchRequest = nameof(PunchRequest);

    [Contract(typeof(IAtomicValueObservable<bool>))]
    public const string IsTargetInRange = nameof(IsTargetInRange);
}

public static class LifeAPI
{
    [Contract(typeof(IAtomicValueObservable<int>))]
    public const string Health = nameof(Health);

    [Contract(typeof(IAtomicValueObservable<bool>))]
    public const string IsDead = nameof(IsDead);
        
    [Contract(typeof(IAtomicAction<int>))]
    public const string TakeDamageAction = nameof(TakeDamageAction);

}

public static class ObjectType
{
    public const string Damagable = nameof(Damagable);
    public const string Player = nameof(Player);
}