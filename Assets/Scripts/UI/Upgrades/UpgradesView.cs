using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesView : MonoBehaviour
{
    [SerializeField] private UpgradeView _upgradeViewPrefab;
    [SerializeField] private Transform _viewsContainer;
    [SerializeField] private Button _closeButton;

    public event Action OnCloseClicked;

    private void Awake()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    public UpgradeView CreateUpgradeView()
    {
        return Instantiate(_upgradeViewPrefab, _viewsContainer);
    }

    private void OnCloseButtonClick()
    {
        OnCloseClicked?.Invoke();
    }
}
