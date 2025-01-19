using System;
using Entities;
using UnityEngine;
using VContainer;

public class ConverterDock : MonoEntityBase
{
    [field: SerializeField] public string Resource { get; private set; }
    
    private Converter _converter;

    [Inject]
    private void Construct(Converter converter)
    {
        _converter = converter;
    }

    public bool CanLoadResource(string resource)
    {
        if (resource != Resource)
            return false;

        return _converter.CanLoadResources;
    }

    public void LoadResource(string resource)
    {
        if (!CanLoadResource(resource))
            throw new ArgumentException("Cannot load resource " + resource);

        _converter.LoadResource();
    }
}
