using System;
using Game.Gameplay.Conveyors;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ConverterView : MonoBehaviour
{
    [SerializeField] private ConveyorVisual _visual;
    [SerializeField] private ZoneVisual _loadZone; 
    [SerializeField] private ZoneVisual _unloadZone;
    [SerializeField] private Image _progress;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _unloadButton;

    public event Action OnLoadResourceRequested;
    public event Action OnUnloadResourceRequested;
    public event Action OnClick
    {
        add { _visual.OnMouseDownEvent += value; }
        remove { _visual.OnMouseDownEvent -= value; }
    }

    private void Awake()
    {
        _loadButton.onClick.AddListener(OnLoadButtonClicked);
        _unloadButton.onClick.AddListener(OnUnloadButtonClicked);
    }

    public void SetProgress(float progress)
    {
        _progress.fillAmount = progress;
    }

    public void Play()
    {
        _visual.Play();
    }

    public void Stop()
    {
        _visual.Stop();
    }

    public void SetLoadedCount(int count)
    {
        _loadZone.SetupItems(count);
    }

    public void SetUnloadedCount(int count)
    {
        _unloadZone.SetupItems(count);
    }

    [Button]
    private void OnLoadButtonClicked()
    {
        OnLoadResourceRequested?.Invoke();
    }

    [Button]
    private void OnUnloadButtonClicked()
    {
        OnUnloadResourceRequested?.Invoke();
    }
}
