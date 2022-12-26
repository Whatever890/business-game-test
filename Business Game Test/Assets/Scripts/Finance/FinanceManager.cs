using UnityEngine;
using Common;

namespace Game.Finance
{
    public static class FinanceManager
    {
        public static int Money
        {
            get => PlayerPrefs.GetInt(Constants.MONEY_KEY, 0);
            private set => PlayerPrefs.SetInt(Constants.MONEY_KEY, value);
        }

        public static event System.Action OnMoneyChanged;
        
        public static void ReceiveMoney(int amount)
        {
            Money += amount;
            OnMoneyChanged?.Invoke();
        }

        public static void SpendMoney(int amount)
        {
            if (amount > Money)
            {
                Debug.LogError("When spending money, an amount bigger than existing money amount was passed");
                Money = 0;
            }
            else
                Money -= amount;
            OnMoneyChanged?.Invoke();
        }
    }
}