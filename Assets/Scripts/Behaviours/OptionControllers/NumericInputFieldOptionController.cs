using System;
using ModMenu.Utils;
using TMPro;

namespace ModMenu.Behaviours.OptionControllers
{
    // a general input field controller for numeric types
    internal class NumericInputFieldOptionController : OptionController
    {
        public TMP_InputField inputField;
        private Type m_optionType;
        private bool m_isIntegralType;
        
        public void OnInputFieldEndEdit(string value) {
            try {
                if (Convert.ChangeType(value, m_optionType) is { } converted) {
                    SetOptionValue(converted);
                }
            }
            catch (Exception e) when (e is OverflowException or FormatException or InvalidCastException) {
                // ignored
            }

            UpdateAppearance();
        }

        protected override void OnOptionAssigned() {
            base.OnOptionAssigned();
            m_optionType = BaseOption.BaseEntry.SettingType;
            m_isIntegralType = m_optionType.IsIntegral();
            
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            inputField.SetTextWithoutNotify(m_isIntegralType ? BaseOption.BoxedValue.ToString() : $"{BaseOption.BoxedValue:F}");
        }
    }
}