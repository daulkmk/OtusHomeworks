using System;
using System.Collections.Generic;
using Lessons.MetaGame.Inventory;
using UnityEngine;
using VContainer;

/// <summary>
/// Presents inventory on screen
/// </summary>
public class ListInventoryPresenter : MonoBehaviour, IInventoryObserver
{
    [SerializeField] private InventoryItemView _itemViewPrefab;
    [SerializeField] private Transform _presentersContainer;

    private readonly Dictionary<InventoryItem, InventoryItemView> _presenterByItem = new();
    private IInventory _inventory;

    public event Action<InventoryItem> OnItemClicked;

    [Inject]    
    public void Construct(IInventory inventory)
    {
        _inventory = inventory;
        _inventory.AddObserver(this);

        foreach (var item in inventory.GetItems())
        {
            ShowItem(item);
        }
    }

    void IInventoryObserver.OnItemAdded(InventoryItem item) => ShowItem(item);
    void IInventoryObserver.OnItemRemoved(InventoryItem item) => HideItem(item);

    private void ShowItem(InventoryItem item)
    {
        var presenter = CreatePresenter(item);
        _presenterByItem.Add(item, presenter);
    }

    private void HideItem(InventoryItem item)
    {
        var presenter = _presenterByItem[item];
        _presenterByItem.Remove(item);

        Destroy(presenter.gameObject);
    }

    private InventoryItemView CreatePresenter(InventoryItem item)
    {
        var presenter = Instantiate(_itemViewPrefab, _presentersContainer);

        presenter.OnClick += () => OnItemClick(presenter);
        presenter.SetItem(item);

        return presenter;
    }

    private void OnItemClick(InventoryItemView presenter)
    {
        OnItemClicked?.Invoke(presenter.InventoryItem);
    }

    private void OnDestroy()
    {
        _inventory?.RemoveObserver(this);
    }
}
