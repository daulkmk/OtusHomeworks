using System;
using UnityEngine;
using VContainer.Unity;

public class Converter : ITickable
{
    private readonly ConverterStats _stats;

    private int _loadedResources = 0;
    private int _unloadedResources = 0;
    private float? _progress = null;

    public int LoadedResourcesCount
    {
        get => _loadedResources;
        set
        {
            _loadedResources = value;
            OnLoadedResourcesChanged?.Invoke();
        }
    }
    public int UnloadedResourcesCount
    {
        get => _unloadedResources;
        set
        {
            _unloadedResources = value;
            OnUnloadedResourcesChanged?.Invoke();
        }
    }

    public bool CanLoadResources => LoadedResourcesCount < _stats.LoadCapacity;
    public bool CanUnloadResources => UnloadedResourcesCount < _stats.UnloadCapacity;
    public bool IsInProgress => _progress.HasValue;
    public float Progress => _progress.GetValueOrDefault(0);

    public event Action OnLoadedResourcesChanged;
    public event Action OnUnloadedResourcesChanged;
    public event Action OnInProgressChanged;

    public Converter(ConverterStats stats)
    {
        _stats = stats;
    }

    public void LoadResource()
    {
        if (CanLoadResources)
        {
            LoadedResourcesCount++;
        }
    }

    public bool GetUnloadedResource()
    {
        if (UnloadedResourcesCount > 0)
        {
            UnloadedResourcesCount--;
            return true;
        }
        return false;
    }

    void ITickable.Tick()
    {
        if (_progress.HasValue)
        {
            ProcessLoadedResource();
        }
        else if (LoadedResourcesCount > 0 && CanUnloadResources)
        {
            StartProcessingLoadedResource();
        }
    }

    private void ProcessLoadedResource()
    {
        if (_progress.Value >= 1)
        {
            if (CanUnloadResources)
            {
                UnloadedResourcesCount++;

                _progress = null;
                OnInProgressChanged?.Invoke();
            }
        }
        else
        {
            _progress += Time.deltaTime * _stats.Speed;
        }
    }

    private void StartProcessingLoadedResource()
    {
        LoadedResourcesCount--;
        _progress = 0;
        OnInProgressChanged?.Invoke();
    }
}
