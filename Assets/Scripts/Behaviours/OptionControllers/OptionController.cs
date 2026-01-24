using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using ModMenu.Options;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModMenu.Behaviours.OptionControllers
{
    internal abstract class OptionController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TextMeshProUGUI nameText;
        public event OptionHoverHandler OnOptionHovered;
        public event OptionHoverHandler OnOptionUnhovered;

        private Option m_baseOption;
        private Tweener m_errorShake; 

        private void Awake() {
            m_errorShake = DOTween.Shake(() => transform.localPosition, p => transform.localPosition = p, 0.2f, 15f * Vector3.right, 20).SetAutoKill(false).Pause();
        }

        public Option BaseOption {
            get => m_baseOption;
            set {
                m_baseOption = value;
                OnOptionAssigned();
            }
        }

        public delegate void OptionHoverHandler();

        public virtual void ResetToDefault() => BaseOption.ResetToDefault();
        
        public void OnPointerEnter(PointerEventData eventData) {
            //PauseManager.Instance.PlayMenuClip(PauseManager.Instance.genericMenuClip);
            OnOptionHovered?.Invoke();
        }
        public void OnPointerExit(PointerEventData eventData) {
            OnOptionUnhovered?.Invoke();
        }

        protected void SetOptionValue(object value) {
            try {
                BaseOption.BoxedValue = value;
            }
            catch (Exception e) {
                m_errorShake?.Restart();
                UpdateAppearance();
                // InvalidOperationException comes from synced config entries and can be safely ignored
                if (e is not InvalidOperationException) {
                    throw;
                }
            }
        }
        
        protected virtual void OnOptionAssigned() {
            nameText.text = BaseOption.Name;
        }
        
        public abstract void UpdateAppearance();
    }

    internal abstract class ValueOptionController<T, TValue> : OptionController where T : ValueOption<TValue>
    {
        protected T Option => BaseOption as T;
    }
}