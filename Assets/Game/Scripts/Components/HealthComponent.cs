using System;
using UnityEngine;
using Atomic.Elements;

[Serializable]
public sealed class HealthComponent
{
    public IAtomicVariableObservable<int> Health => _health;
    public IAtomicAction<int> TakeDamageAction => _takeDamageAction;
    public IAtomicObservable DeathEvent => _deathEvent;

    [SerializeField] private AtomicVariable<int> _health;
    [SerializeField] private AtomicAction<int> _takeDamageAction;
    [SerializeField] private AtomicEvent _deathEvent;

    public void Compose()
    {
        _takeDamageAction.Compose(TakeDamage);
        _health.Subscribe(OnHealthChanged);
    }

    private void TakeDamage(int damage) => _health.Value -= damage;

    private void OnHealthChanged(int value)
    {
        if (value <= 0) 
            _deathEvent.Invoke();
    }
}