using System;
using Entities;

public class LifeComponent
{
    private readonly MonoEntityBase _entity;

    public bool IsAlive { get; private set; } = true;
    public event Action<IEntity> OnDeath;

    private event Action OnDied;

    public LifeComponent(MonoEntityBase entity, Action onDied)
    {
        _entity = entity;
        OnDied = onDied;
    }

    public void DieImmediatly()
    {
        IsAlive = false;
        OnDeath?.Invoke(_entity);

        OnDied?.Invoke();
    }
}