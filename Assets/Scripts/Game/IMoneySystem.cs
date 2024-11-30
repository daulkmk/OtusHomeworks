using System;

public interface IMoneySystem
{
    int Money { get; }

    event Action OnMoneyChanged;
    
    bool CanSpendMoney(int money);
    void SpendMoney(int money);

    void AddMoney(int value);
    void SetMoney(int value);
}
