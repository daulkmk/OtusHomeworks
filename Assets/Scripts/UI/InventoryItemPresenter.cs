using System;
using Lessons.MetaGame.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemPresenter : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;
    [SerializeField] private Text _description;
    [SerializeField] private Button _button;

    public event Action OnClick;

    public InventoryItem InventoryItem{ get; private set; }

    private void Awake()
    {
        _button.onClick.AddListener(() => OnClick?.Invoke());
    }

    public void SetItem(InventoryItem inventoryItem)
    {
        InventoryItem = inventoryItem;

        _icon.enabled = true;
        _icon.sprite = inventoryItem.Metadata.icon;
        _title.text = inventoryItem.Metadata.title;
        _description.text = inventoryItem.Metadata.description;
    }

    public void ResetValues()
    {
        InventoryItem = null;
        
        _icon.sprite = null;
        _title.text = string.Empty;
        _description.text = string.Empty;

        _icon.enabled = false;
    }
}
