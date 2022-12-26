using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Business
{
    public class BusinessView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Image _incomeProgressFill;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _income;
        [SerializeField] private GameObject _shadow;

        public void SetEnabled(bool on) =>
            _shadow.SetActive(!on);

        public void SetTitle(string title) =>
            _title.text = title;

        public void SetLevel(int lvl) =>
            _level.text = lvl.ToString();

        public void SetIncome(int income) =>
            _income.text = "$" + System.String.Format("{0:n0}", income);

        public void SetIncomeProgress(float value) =>
            _incomeProgressFill.fillAmount = value;
    }
}