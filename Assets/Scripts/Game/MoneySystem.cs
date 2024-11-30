using System;

public class MoneySystem : IMoneySystem
{
    private int _money = 10;

    public int Money => _money;
    public event Action OnMoneyChanged;

    public MoneySystem(int initialMoneyCount)
    {
        _money = initialMoneyCount;
    }

    public bool CanSpendMoney(int value) => Money > value;

    public void SpendMoney(int money)
    {
        if (!CanSpendMoney(money))
            throw new System.Exception("Cant spend money " + money);

        _money -= money;
        OnMoneyChanged?.Invoke();
    }

    public void AddMoney(int value)
    {
        if (value < 0)
            throw new System.ArgumentException("Negative money value to add " + value.ToString());

        _money += value;
        OnMoneyChanged?.Invoke();
    }

    public void SetMoney(int value)
    {
        _money = 0;
        AddMoney(value);
    }
}
