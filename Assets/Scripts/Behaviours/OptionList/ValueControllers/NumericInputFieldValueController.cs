using System;
using ModMenu.Utils;
using TMPro;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public class NumericInputFieldValueController : BoxedValueController
    {
        public TMP_InputField inputField;

        private bool m_isIntegralType;

        protected override void Setup() {
            m_isIntegralType = ValueType.IsIntegral();
        }

        public void OnInputFieldEndEdit(string value) {
            try {
                if (Convert.ChangeType(value, ValueType) is { } converted) {
                    boxedSetter(converted);
                }
            }
            catch (Exception ex) when (ex is OverflowException or FormatException or InvalidCastException) {
                // ignored
            }
            
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            inputField.SetTextWithoutNotify(m_isIntegralType? boxedGetter().ToString() : $"{boxedGetter():F}");
        }
    }
}