using UnityEngine;
using TMPro;
using Game.Finance;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counter;

    private void Awake()
    {
        FinanceManager.OnMoneyChanged += SetCount;
        SetCount();
    }

    private void SetCount()
    {
        int count = FinanceManager.Money;
        _counter.text = "$" + System.String.Format("{0:n0}", count);
    }
}