using Entities;
using VContainer.Unity;
using UnityEngine;

public class Tree : MonoEntityBase, IInitializable
{
    [field: SerializeField] public ResourceComponent ResourceComponent { get; private set; }
    public LifeComponent LifeComponent{ get; private set; }

    public void Initialize()
    {
        LifeComponent = new LifeComponent(this, OnDied);

        Add(LifeComponent);
        Add(ResourceComponent);
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }
}