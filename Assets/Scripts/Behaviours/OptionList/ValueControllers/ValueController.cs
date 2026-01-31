using System;
using DG.Tweening;
using ModMenu.Options;
using UnityEngine;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    /// <summary>
    /// An option list item that interacts with a boxed "value" via a getter and setter
    /// </summary>
    public abstract class BoxedValueController : OptionListItem
    {
        public Func<object> getter;
        public Action<object> setter;
        
        private object m_defaultValue;
        private Tweener m_errorShake;
        
        public Type ValueType { get; private set; }
        
        private void Start() {
            m_errorShake = DOTween.Shake(() => transform.localPosition, p => transform.localPosition = p, 0.2f, 15f * Vector3.right, 20).SetAutoKill(false).Pause();
        }
        
        public virtual void ResetValue() => setter(m_defaultValue);
        public abstract void UpdateAppearance();
        protected virtual void Setup() { }

        internal virtual void SetupFromOption(Option option) {
            m_defaultValue = option.BaseEntry.DefaultValue;
            ValueType = option.BaseEntry.SettingType;
            
            getter = () => option.BoxedValue;
            setter = value => {
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
            m_defaultValue = default(T);
            
            ValueType = typeof(T);
            this.getter = () => getter();
            this.setter = value => setter((T)value);
            
            Setup();
            UpdateAppearance();
        }
    }
    
    /// <summary>
    /// Provides typed versions of a <see cref="BoxedValueController"/>'s getter and setter
    /// </summary>
    public abstract class ValueController<T> : BoxedValueController
    {
        public T GetValue() => (T)getter();
        public void SetValue(T value) => setter(value);
    }
}