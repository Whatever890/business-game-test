using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Business
{
    public class BusinessEnhancementView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _income;

        [Header("Sprites")]
        [SerializeField] private Sprite _acquiredSprite;

        public void SetTitle(string title) =>
            _title.text = title;

        public void SetIncome(int income) =>
            _income.text = $"Income: +{income.ToString()}%";

        public void SetAcquired() =>
            _image.sprite = _acquiredSprite;
    }
}