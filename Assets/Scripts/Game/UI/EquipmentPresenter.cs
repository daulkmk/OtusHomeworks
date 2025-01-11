using System;
using System.Collections.Generic;
using Lessons.MetaGame.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;


/// <summary>
/// Presents equpment on screen
/// </summary>
public class EquipmentPresenter : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<EquipSlot, InventoryItemView> _viewBySlot = new();
    private IEquipment _equipment;

    public event Action<InventoryItem> OnItemClicked;

    private void Awake()
    {
        foreach (var presenter in _viewBySlot.Values)
        {
            presenter.OnClick += () => OnItemClick(presenter);
        }   
    }

    [Inject]    
    public void Construct(IEquipment equipment)
    {
        _equipment = equipment;

        _equipment.OnItemEquipped += OnItemEquipped;
        _equipment.OnItemUnequipped += OnItemUnequipped;

        foreach (var (slot, item) in _equipment.EquippedItems)
        {
            OnItemEquipped(slot, item);
        }
    }

    private void OnItemClick(InventoryItemView presenter)
    {
        if (presenter.InventoryItem != null)
        {
            OnItemClicked?.Invoke(presenter.InventoryItem);
        }
    }

    private void OnItemEquipped(EquipSlot slot, InventoryItem item)
    {
        var itemView = _viewBySlot[slot];
        itemView.SetItem(item);
    }

    private void OnItemUnequipped(EquipSlot slot, InventoryItem item)
    {
        var itemView = _viewBySlot[slot];
        itemView.ResetValues();
    }

    private void OnDestroy()
    {
        if (_equipment != null)
        {
            _equipment.OnItemEquipped -= OnItemEquipped;
            _equipment.OnItemUnequipped -= OnItemUnequipped;
        }
    }
}
