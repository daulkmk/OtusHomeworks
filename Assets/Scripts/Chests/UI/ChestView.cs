using System;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Text _statusText;

    [SerializeField] private GameObject _closed;
    [SerializeField] private GameObject _open;

    public event Action OnOpenButtonClick;

    private void Start()
    {
        _openButton.onClick.AddListener(OnOpenButtonClicked);
    }

    private void OnOpenButtonClicked()
    {
        OnOpenButtonClick?.Invoke();
    }

    public void SetStatus(string status) => _statusText.text = status;

    public void Close() => UpdateOpenClose(isOpen: false);
    public void Open() => UpdateOpenClose(isOpen: true);

    public void SetIsReadyForOpen(bool isReady)
    {
        _openButton.gameObject.SetActive(isReady);
    }

    private void UpdateOpenClose(bool isOpen)
    {
        _open.SetActive(isOpen);
        _closed.SetActive(!isOpen);
    }
}
