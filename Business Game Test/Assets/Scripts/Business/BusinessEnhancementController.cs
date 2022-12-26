using UnityEngine;
using Game.UI;
using Game.Finance;

namespace Game.Business
{
    public class BusinessEnhancementController : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private BusinessEnhancementView _view;
        
        [Header("Buttons")]
        [SerializeField] private PaidButton _buyButton;

        private BusinessEnhancementModel _businessEnhancementModel;

        public event System.Action OnAcquired;

        public void Init(BusinessEnhancementModel businessEnhancementModel)
        {
            // Assign model
            _businessEnhancementModel = businessEnhancementModel;

            // Prepare events
            FinanceManager.OnMoneyChanged += SetAffordableStatus;

            // Set view
            _view.SetTitle(businessEnhancementModel.Name);
            _view.SetIncome((int)(businessEnhancementModel.IncomeMultiplyFactor * 100f));
            
            // Set buttons
            if (!_businessEnhancementModel.IsAcquired)
            {
                _buyButton.OnPressed += Buy;
                SetAffordableStatus();
                _buyButton.SetPrice(_businessEnhancementModel.Price);
            }
            else
                SetAcquired();
        }

        // Checks whether buy button is affordable and sets it to corresponding status
        private void SetAffordableStatus()
        {
            int price = _businessEnhancementModel.Price;
            bool affordable = FinanceManager.Money >= price;
            _buyButton.SetAvailable(affordable);
        }

        // Buys enhancement
        private void Buy()
        {
            SetAcquired();
            int price = _businessEnhancementModel.Price;
            FinanceManager.SpendMoney(price);
            _businessEnhancementModel.SetAcquired();
            OnAcquired?.Invoke();
        }

        // Set enhancement to acquired
        private void SetAcquired()
        {
            FinanceManager.OnMoneyChanged -= SetAffordableStatus;
            _view.SetAcquired();
            _buyButton.SetInteractable(false);
            _buyButton.SetMessage("Bought");
        }
    }
}