using System;
using ModMenu.Utils;
using TMPro;
using UnityEngine.UI;

namespace ModMenu.Behaviours.OptionControllers
{
    // a general slider controller for numeric types
    public class NumericSliderOptionController : OptionController
    {
        public Slider slider;
        public TMP_InputField inputField;
        private Type m_optionType;
        private bool m_isIntegralType;

        public void OnSliderValueChanged(float value) {
            if (Convert.ChangeType(value, m_optionType) is { } converted) {
                BaseOption.BoxedValue = converted;
            }
            UpdateAppearance();
        }

        // don't need to clamp here as bepin does that for us :3 thanks bepin
        public void OnInputFieldEndEdit(string value) {
            try {
                if (Convert.ChangeType(value, m_optionType) is { } converted) {
                    BaseOption.BoxedValue = converted;
                }
            }
            catch (Exception e) when (e is OverflowException or FormatException or InvalidCastException) {
                // ignored
            }
    
            UpdateAppearance();
        }

        protected override void OnSetOption() {
            base.OnSetOption();
            m_optionType = BaseOption.BaseEntry.SettingType;
            m_isIntegralType = m_optionType.IsIntegral();

            var range = BaseOption.AcceptableValues;
            var valueRangeType = range.GetType();
            
            slider.minValue = Convert.ToSingle(valueRangeType.GetProperty("MinValue")!.GetValue(range));
            slider.maxValue = Convert.ToSingle(valueRangeType.GetProperty("MaxValue")!.GetValue(range));
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            slider.SetValueWithoutNotify(Convert.ToSingle(BaseOption.BoxedValue));
            inputField.SetTextWithoutNotify(m_isIntegralType ? BaseOption.BoxedValue.ToString() : $"{BaseOption.BoxedValue:F}");
        }
    }
}