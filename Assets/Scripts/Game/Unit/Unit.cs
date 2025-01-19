using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using VContainer.Unity;

public class Unit : MonoEntityBase, IInitializable, ITickable, IDisposable
{
    [SerializeField] private float _initialMoveSpeed = 5;

    public MoveComponent MoveComponent { get;  private set; }
    public LookDirectionComponent LookDirectionComponent { get;  private set; }
    public InventoryComponent InventoryComponent { get;  private set; }
    public CollectResourceComponent CollectResourceComponent { get; private set; }
    [field: SerializeField] public TriggerComponent TriggerComponent { get; private set; }

    protected readonly List<ITickable> _tickables = new();
    protected readonly List<IDisposable> _disposables = new();

    public void Initialize()
    {
        MoveComponent = new MoveComponent(transform) { Speed = _initialMoveSpeed };
        LookDirectionComponent = new LookDirectionComponent(transform);
        InventoryComponent = new InventoryComponent();
        CollectResourceComponent = new CollectResourceComponent(InventoryComponent);

        Add(MoveComponent);
        Add(LookDirectionComponent);
        Add(InventoryComponent);
        Add(InventoryComponent);
        Add(TriggerComponent);
        Add(CollectResourceComponent);

        _tickables.Add(MoveComponent);
        _tickables.Add(LookDirectionComponent);

        OnInitialize();
    }

    protected virtual void OnInitialize() { }

    public void Tick()
    {
        _tickables.ForEach(t => t.Tick());
    }

    public void Dispose()
    {
        _disposables.ForEach(d => d.Dispose());
    }
}
