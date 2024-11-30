using System;
using VContainer.Unity;

public class NavigationController : IInitializable, IDisposable
{
    private readonly IUpgradesViewController _upgradesViewController;
    private readonly IConverterViewController _converterViewController;

    public NavigationController(IUpgradesViewController upgradesViewController, IConverterViewController converterViewController)
    {
        _upgradesViewController = upgradesViewController;
        _converterViewController = converterViewController;
    }

    void IInitializable.Initialize()
    {
        _upgradesViewController.SetActive(false);
        _converterViewController.OnClick += OnConverterClick;
    }

    void IDisposable.Dispose()
    {
        _converterViewController.OnClick -= OnConverterClick;
    }

    private void OnConverterClick()
    {
        _upgradesViewController.SetActive(true);
    }
}
