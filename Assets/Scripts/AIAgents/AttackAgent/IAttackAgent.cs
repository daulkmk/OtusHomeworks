using R3;

namespace ShootEmUp
{
    public interface IAttackAgent
    {
        ReactiveProperty<bool> CanAttack { get; }
        void SetTarget(ITransform target);
        void Reset();
    }
}