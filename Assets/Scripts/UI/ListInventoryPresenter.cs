using System;
using System.Collections.Generic;
using Lessons.MetaGame.Inventory;
using UnityEngine;
using VContainer;

public class ListInventoryPresenter : MonoBehaviour, IInventoryObserver
{
    [SerializeField] private InventoryItemPresenter _itemPresenterPrefab;
    [SerializeField] private Transform _presentersContainer;

    private readonly Dictionary<InventoryItem, InventoryItemPresenter> _presenterByItem = new();
    private ListInventory _listInventory;

    public event Action<InventoryItem> OnItemClicked;

    [Inject]    
    public void Construct(ListInventory inventory)
    {
        _listInventory = inventory;
        _listInventory.AddObserver(this);

        foreach (var item in inventory.GetItems())
        {
            OnItemAdded(item);
        }
    }

    public void OnItemAdded(InventoryItem item)
    {
        var presenter = CreatePresenter(item);
        _presenterByItem.Add(item, presenter);
    }

    public void OnItemRemoved(InventoryItem item)
    {
        var presenter = _presenterByItem[item];
        _presenterByItem.Remove(item);

        Destroy(presenter.gameObject);
    }

    private InventoryItemPresenter CreatePresenter(InventoryItem item)
    {
        var presenter = Instantiate(_itemPresenterPrefab, _presentersContainer);

        presenter.OnClick += () => OnItemClick(presenter);
        presenter.SetItem(item);

        return presenter;
    }

    private void OnItemClick(InventoryItemPresenter presenter)
    {
        OnItemClicked?.Invoke(presenter.InventoryItem);
    }

    private void OnDestroy()
    {
        _listInventory?.RemoveObserver(this);
    }
}
