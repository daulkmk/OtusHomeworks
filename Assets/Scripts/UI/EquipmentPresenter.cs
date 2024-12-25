using System;
using System.Collections.Generic;
using Game.GameEngine.Mechanics;
using Lessons.MetaGame.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

public class EquipmentPresenter : SerializedMonoBehaviour, IInventoryObserver
{
    [SerializeField] private Dictionary<EquipType, InventoryItemPresenter> _slots = new();
    private Equipment _equipment;

    public event Action<InventoryItem> OnItemClicked;


    private void Awake()
    {
        foreach (var presenter in _slots.Values)
        {
            presenter.OnClick += () => OnItemClick(presenter);
        }   
    }

    [Inject]    
    public void Construct(Equipment equipment)
    {
        _equipment = equipment;
        _equipment.AddObserver(this);
    }

    private void OnItemClick(InventoryItemPresenter presenter)
    {
        if (presenter.InventoryItem != null)
        {
            OnItemClicked?.Invoke(presenter.InventoryItem);
        }
    }

    public void OnItemAdded(InventoryItem item)
    {
        var itemPresenter = _slots[item.GetComponent<Component_EquipType>().Type];
        itemPresenter.SetItem(item);
    }

    public void OnItemRemoved(InventoryItem item)
    {
        var itemPresenter = _slots[item.GetComponent<Component_EquipType>().Type];
        itemPresenter.ResetValues();
    }

    private void OnDestroy()
    {
        _equipment?.RemoveObserver(this);
    }
}
