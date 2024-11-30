using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private Text _title;
    [SerializeField] private Text _level;
    [SerializeField] private Text _value;

    [SerializeField] private Text _price;
    [SerializeField] private Image _icon;

    [SerializeField] private Button _upgradeButton;

    [SerializeField] private GameObject _maxedState;
    [SerializeField] private GameObject _upgradableState;

    public event Action OnUpgradeClick;

    private void Awake()
    {
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    private void OnUpgradeButtonClick()
    {
        OnUpgradeClick?.Invoke();
    }

    public void SetTitle(string value) => _title.text = value;
    public void SetLevel(string value) => _level.text = value;
    public void SetValue(string value) => _value.text = value;
    public void SetPrice(string value) => _price.text = value;
    public void SetIcon(Sprite sprite) => _icon.sprite = sprite;

    public void SetIsMaxed(bool isMaxed)
    {
        _maxedState.SetActive(isMaxed);
        _upgradableState.SetActive(!isMaxed);
    }

    public void SetCanLevelUp(bool canLevelUp)
    {
        _upgradeButton.interactable = canLevelUp;
    }
}
