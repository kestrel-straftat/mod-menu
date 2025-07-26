using ModMenu.Options;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModMenu.Behaviours.OptionControllers
{
    public abstract class OptionController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TextMeshProUGUI nameText;
        public event OptionHoverHandler OnOptionHovered;
        public event OptionHoverHandler OnOptionUnhovered;
        
        protected Option BaseOption { get; set; }

        public delegate void OptionHoverHandler();

        public virtual void ResetToDefault() => BaseOption.ResetToDefault();
        
        public void SetOption(Option option) {
            BaseOption = option;
            OnSetOption();
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            //PauseManager.Instance.PlayMenuClip(PauseManager.Instance.genericMenuClip);
            OnOptionHovered?.Invoke();
        }
        public void OnPointerExit(PointerEventData eventData) {
            OnOptionUnhovered?.Invoke();
        }

        protected virtual void OnSetOption() {
            nameText.text = BaseOption.Name;
        }
        public abstract void UpdateAppearance();
    }

    public abstract class ValueOptionController<T, TValue> : OptionController where T : ValueOption<TValue>
    {
        protected T Option => BaseOption as T;
    }
}