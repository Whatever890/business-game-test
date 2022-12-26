using UnityEngine;
using Game.UI;
using Game.Finance;

namespace Game.Business
{
    public class BusinessController : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private BusinessView _view;
        
        [Header("Buttons")]
        [SerializeField] private PaidButton _upgradeButton;

        [Header("Construction")]
        [SerializeField] private Transform _enhancementsContainer;
        [SerializeField] private BusinessEnhancementController _enhancementPrefab;

        private BusinessModel _businessModel;

        private bool _acquired;

        private float _totalIncomeTime;
        private float _leftIncomeTime;

        public void Init(BusinessModel businessModel)
        {
            // Assign model
            _businessModel = businessModel;

            // Prepare events
            FinanceManager.OnMoneyChanged += SetUpgradableStatus;

            // Set view
            _view.SetTitle(businessModel.Name);
            RefreshInfo();

            // Set buttons
            _upgradeButton.OnPressed += Upgrade;
            SetUpgradableStatus();
            _upgradeButton.SetPrice(businessModel.GetUpgradePrice());

            // Set enhancements
            foreach (BusinessEnhancementModel enhancement in businessModel.Enhancements)
            {
                BusinessEnhancementController controller = Instantiate(_enhancementPrefab, _enhancementsContainer);
                controller.Init(enhancement);
                if (!enhancement.IsAcquired)
                    controller.OnAcquired += RefreshInfo;
            }

            // Set income progress
            _totalIncomeTime = businessModel.IncomeTime;
            _leftIncomeTime = businessModel.LeftIncomeTime;
            VisualizeIncomeProgress();
        }

        private void RefreshInfo()
        {
            _acquired = _businessModel.Level > 0;
            _view.SetEnabled(_acquired);
            _view.SetLevel(_businessModel.Level);
            _view.SetIncome(_businessModel.GetIncome());
        }

        private void SetUpgradableStatus()
        {
            int price = _businessModel.GetUpgradePrice();
            bool upgradable = FinanceManager.Money >= price;
            _upgradeButton.SetAvailable(upgradable);
        }

        private void Upgrade()
        {
            int price = _businessModel.GetUpgradePrice();
            FinanceManager.SpendMoney(price);
            _businessModel.Upgrade();
            RefreshInfo();
            _upgradeButton.SetPrice(_businessModel.GetUpgradePrice());
        }

        private void Update()
        {
            if (_acquired)
                SetIncomeProgress();
        }

        private void SetIncomeProgress()
        {
            if (_leftIncomeTime > 0f)
            {
                _leftIncomeTime -= Time.deltaTime;
            }
            else
            {
                _leftIncomeTime = _totalIncomeTime;
                GetIncome();
            }
            
            VisualizeIncomeProgress();
            _businessModel.SetLeftIncomeTime(_leftIncomeTime);
        }

        private void VisualizeIncomeProgress()
        {
            float progressValue = 1f - ((1f / _totalIncomeTime) * _leftIncomeTime);
            _view.SetIncomeProgress(progressValue);
        }

        private void GetIncome()
        {
            int income = _businessModel.GetIncome();
            FinanceManager.ReceiveMoney(income);
        }
    }
}