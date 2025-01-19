using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class TriggerComponent : MonoBehaviour
{
    private readonly HashSet<IEntity> _entitiesInRange = new();

    public IReadOnlyCollection<IEntity> EntitiesInRange => _entitiesInRange;

    public event Action<Collider, IEntity> OnTriggerEntered;
    public event Action<Collider, IEntity> OnTriggerExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IEntity>(out var entity))
            _entitiesInRange.Add(entity);

        OnTriggerEntered?.Invoke(other, entity);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IEntity>(out var entity))
            _entitiesInRange.Remove(entity);

        OnTriggerExited?.Invoke(other, entity);
    }
}
