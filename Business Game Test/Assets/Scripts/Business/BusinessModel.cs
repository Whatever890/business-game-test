using System.Linq;
using UnityEngine;
using Common;

namespace Game.Business
{
    [CreateAssetMenu(fileName = "New BusinessModel", menuName = "Game/BusinessModel")]
    public class BusinessModel : ScriptableObject
    {
        [Header("Configs")]
        [SerializeField] private string _name;
        [SerializeField] private int _incomeTimeSeconds;
        [SerializeField] private int _basePrice;
        [SerializeField] private int _baseIncome;

        [Header("Enhancements")]
        [SerializeField] private BusinessEnhancementModel[] _enhancements;

        [Header("Initial Data")]
        [SerializeField] private bool _initiallyAcquired = false;

        [Header("DataBase")]
        // Permanent unique ID for object used to dynamically receive data from PlayerPrefs. Generated automatically.
        [SerializeField, ScriptableObjectIdAttribute] private string _id;

        // Properties
        public string Name => _name;
        public int IncomeTime => _incomeTimeSeconds;
        public int BasePrice => _basePrice;
        public int BaseIncome => _baseIncome;
        public BusinessEnhancementModel[] Enhancements => _enhancements;
        public bool InitiallyAcquired => _initiallyAcquired;

        // Load level from PlayerPrefs by _id
        public int Level => PlayerPrefs.GetInt(_id + Constants.LEVEL_KEY, 0);
        // Load left time until income from PlayerPrefs by _id
        public float LeftIncomeTime => PlayerPrefs.GetFloat(_id + Constants.FLOAT_TIME_KEY, (float)_incomeTimeSeconds);

        public int GetUpgradePrice() =>
            _basePrice * (Level + 1);

        // Increase and set level with PlayerPrefs by _id
        public void Upgrade() =>
            PlayerPrefs.SetInt(_id + Constants.LEVEL_KEY, this.Level+1);

        // Set left time until income with PlayerPrefs by _id
        public void SetLeftIncomeTime(float leftTime) =>
            PlayerPrefs.SetFloat(_id + Constants.FLOAT_TIME_KEY, leftTime);

        // Gets Total income based on the upgrade level and enhancements
        public int GetIncome()
        {
            float multiplyFactor = 1f;
            _enhancements.ToList().ForEach(e => multiplyFactor += e.IsAcquired ? e.IncomeMultiplyFactor : 0f);
            return (int)(_baseIncome * Level * multiplyFactor);
        }
    }

    [System.Serializable]
    public class BusinessEnhancementModel
    {
        [Header("Configs")]
        [SerializeField] private string _name;
        [SerializeField] private int _price;
        [SerializeField] private float _incomeMultiplyFactor;

        [Header("DataBase")]
        // Permanent unique ID for object used to dynamically receive data from PlayerPrefs. Generated automatically.
        [SerializeField, ScriptableObjectIdAttribute] private string _id;

        // Properties
        public string Name => _name;
        public int Price => _price;
        public float IncomeMultiplyFactor => _incomeMultiplyFactor;

        // Load whether enhancement is acquired from PlayerPrefs by _id
        public bool IsAcquired => PlayerPrefs.GetInt(_id + Constants.ACQUIRED_KEY, 0) == 1;

        // Save whether enhancement is acquired in PlayerPrefs by _id
        public void SetAcquired() =>
            PlayerPrefs.SetInt(_id + Constants.ACQUIRED_KEY, 1);
    }
}