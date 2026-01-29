using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModMenu.Behaviours.OptionList
{
    public class OptionListItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public delegate void ItemHoverHandler();
        
        public TextMeshProUGUI nameText;
        
        public string NameText {
            get => nameText.text;
            set => nameText.text = value;
        }
        
        public event ItemHoverHandler OnItemHovered;
        public event ItemHoverHandler OnItemUnhovered;
        
        public void OnPointerEnter(PointerEventData eventData) => OnItemHovered?.Invoke();
        public void OnPointerExit(PointerEventData eventData) => OnItemUnhovered?.Invoke();
    }
}