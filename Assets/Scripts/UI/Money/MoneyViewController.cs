using System;
using VContainer.Unity;

public class MoneyViewController : IInitializable, IDisposable
{
    private readonly MoneyView _view;
    private readonly IMoneySystem _moneySystem;

    public MoneyViewController(MoneyView view, IMoneySystem moneySystem)
    {
        _view = view;
        _moneySystem = moneySystem;
    }

    void IInitializable.Initialize()
    {
        UpdateView();

        _moneySystem.OnMoneyChanged += OnMoneyChanged;
    }

    void IDisposable.Dispose()
    {
        _moneySystem.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged() => UpdateView();

    private void UpdateView()
    {
        _view.SetMoneyCount(_moneySystem.Money.ToString());
    }
}
