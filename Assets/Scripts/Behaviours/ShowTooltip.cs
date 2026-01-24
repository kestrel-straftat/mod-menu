using UnityEngine;
using UnityEngine.EventSystems;

namespace ModMenu.Behaviours
{
    internal class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string textToShow;
        
        public void OnPointerEnter(PointerEventData eventData) {
            FloatingName.Instance.nameToShow = textToShow;
        }
        public void OnPointerExit(PointerEventData eventData) {
            FloatingName.Instance.nameToShow = "";
        }
    }
}