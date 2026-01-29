using System;
using ModMenu.Options;
using ModMenu.Utils;
using TMPro;
using Slider = UnityEngine.UI.Slider;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public class NumericSliderValueController : BoxedValueController
    {
        public Slider slider;
        public TMP_InputField inputField;
        
        private bool m_isIntegralType;

        protected override void Setup() {
            m_isIntegralType = ValueType.IsIntegral();
        }

        internal override void SetupFromOption(Option option) {
            var range = option.AcceptableValues;
            var valueRangeType = range.GetType();
            slider.minValue = Convert.ToSingle(valueRangeType.GetProperty("MinValue")!.GetValue(range));
            slider.maxValue = Convert.ToSingle(valueRangeType.GetProperty("MaxValue")!.GetValue(range));
            base.SetupFromOption(option);
        }

        public void OnSliderValueChanged(float value) {
            if (Convert.ChangeType(value, ValueType) is { } converted) {
                BoxedSetter(converted);
            }
            
            UpdateAppearance();
        }

        // don't need to clamp here as bepin does that for us :3 thanks bepin
        public void OnInputFieldEndEdit(string value) {
            try {
                if (Convert.ChangeType(value, ValueType) is { } converted) {
                    BoxedSetter(converted);
                }
            }
            catch (Exception e) when (e is OverflowException or FormatException or InvalidCastException) {
                // ignored
            }
    
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            var value = BoxedGetter();
            slider.SetValueWithoutNotify(Convert.ToSingle(value));
            inputField.SetTextWithoutNotify(m_isIntegralType ? value.ToString() : $"{value:F}");
        }
    }
}