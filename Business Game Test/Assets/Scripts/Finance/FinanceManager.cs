using UnityEngine;
using Common;

namespace Game.Finance
{
    public static class FinanceManager
    {
        // Current money value loaded/uploaded directly from/to PLayerPrefs
        public static int Money
        {
            get => PlayerPrefs.GetInt(Constants.MONEY_KEY, 0);
            private set => PlayerPrefs.SetInt(Constants.MONEY_KEY, value);
        }

        // Event to subscribe on in order to update info that depends on the amount of money
        public static event System.Action OnMoneyChanged;
        
        // Adds amount of money and saves data
        public static void ReceiveMoney(int amount)
        {
            Money += amount;
            OnMoneyChanged?.Invoke();
        }

        // Deducts amount of money and saves data
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