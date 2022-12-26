using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Game.UI
{
    public class PaidButton : MonoBehaviour, IPointerClickHandler
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Image _image;

        [Header("Colors")]
        [SerializeField] private Color _uninteractableColor = Color.gray;
        [SerializeField] private Color _availableTextColor = Color.white;
        [SerializeField] private Color _unavailableTextColor = Color.red;

        [Header("Additional")]
        [SerializeField] private string _textBeforePrice = "";
        [SerializeField] private string _textAfterPrice = "";

        private bool _interactable = false;
        public event System.Action OnPressed;

        public void SetPrice(int price) =>
            _price.text = _textBeforePrice + System.String.Format("{0:n0}", price) + _textAfterPrice;

        public void SetMessage(string message) =>
            _price.text = message;

        public void SetAvailable(bool on)
        {
            SetInteractable(on);
            _price.color = on ? _availableTextColor : _unavailableTextColor;
        }

        public void SetInteractable(bool on)
        {
            _interactable = on;
            _image.color = on ? Color.white : _uninteractableColor;
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (!_interactable)
                return;

            OnPressed?.Invoke();
        }
    }
}