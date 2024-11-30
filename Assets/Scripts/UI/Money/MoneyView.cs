using UnityEngine;
using UnityEngine.UI;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private Text _moneyCount;

    public void SetMoneyCount(string moneyCount) => _moneyCount.text = moneyCount;
}
