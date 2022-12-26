using UnityEngine;

namespace Game.Business
{
    public class BusinessManager : MonoBehaviour
    {
        [Header("Models")]
        [SerializeField] private BusinessModel[] _businesses;

        [Header("Construction")]
        [SerializeField] private Transform _businessContainer;
        [SerializeField] private BusinessController _businessSlotPrefab;

        private void Start()
        {
            foreach (BusinessModel business in _businesses)
            {
                // Set business to level 1 if it's set to be initially acquired
                if (business.InitiallyAcquired && business.Level == 0)
                    business.Upgrade();

                // Create business slot and assign business model to it
                BusinessController controller = Instantiate(_businessSlotPrefab, _businessContainer);
                controller.Init(business);
            }
        }
    }
}