using System;
using DG.Tweening;
using ModMenu.Options;
using UnityEngine;
using Object = System.Object;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public abstract class BoxedValueController : OptionListItem
    {
        private object m_defaultValue;
        private Tweener m_errorShake; 
        
        public Func<object> BoxedGetter { get; private set; }
        public Action<Object> BoxedSetter { get; private set; }
        public Type ValueType { get; private set; }
        
        private void Start() {
            m_errorShake = DOTween.Shake(() => transform.localPosition, p => transform.localPosition = p, 0.2f, 15f * Vector3.right, 20).SetAutoKill(false).Pause();
        }
        
        public virtual void ResetValue() => BoxedSetter(m_defaultValue);
        public abstract void UpdateAppearance();
        protected virtual void Setup() { }

        internal virtual void SetupFromOption(Option option) {
            m_defaultValue = option.BaseEntry.DefaultValue;
            ValueType = option.BaseEntry.SettingType;
            
            BoxedGetter = () => option.BoxedValue;
            BoxedSetter = value => {
                try {
                    option.BoxedValue = value;
                }
                catch (Exception ex) {
                    m_errorShake?.Restart();
                    UpdateAppearance();
                    // InvalidOperationException comes from synced config entries and can be safely ignored
                    if (ex is not InvalidOperationException) {
                        throw;
                    }
                }
            };
            
            Setup();
            UpdateAppearance();
        }

        internal virtual void SetupFromValues<T>(Func<T> getter, Action<T> setter) {
            ValueType = typeof(T);
            BoxedGetter = () => getter();
            BoxedSetter = value => setter((T)value);
        }
    }
    
    public abstract class ValueController<T> : BoxedValueController
    {
        public T Getter() => (T)BoxedGetter();
        public void Setter(T value) => BoxedSetter(value);
    }
}