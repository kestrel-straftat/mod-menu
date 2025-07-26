using UnityEngine;
using UnityEngine.EventSystems;

namespace ModMenu.Behaviours
{
    public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string textToShow;
        
        public void OnPointerEnter(PointerEventData eventData) {
            if (textToShow != "")
                FloatingName.Instance.nameToShow = textToShow;
        }
        public void OnPointerExit(PointerEventData eventData) {
            FloatingName.Instance.nameToShow = "";
        }
    }
}