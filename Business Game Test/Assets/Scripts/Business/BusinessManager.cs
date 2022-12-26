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
                if (business.InitiallyAcquired && business.Level == 0)
                    business.Upgrade();

                BusinessController controller = Instantiate(_businessSlotPrefab, _businessContainer);
                controller.Init(business);
            }
        }
    }
}