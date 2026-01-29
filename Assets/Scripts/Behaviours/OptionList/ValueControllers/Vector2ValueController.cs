using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    internal class Vector2ValueController : ValueController<Vector2>
    {
        public TMP_InputField xInputField;
        public TMP_InputField yInputField;
        
        public void OnInputFieldEndEdit(string value) {
            var originalValue = GetValue();
            float x = originalValue.x;
            float y = originalValue.y;

            if (float.TryParse(xInputField.text, out var newX)) {
                x = newX;
            }

            if (float.TryParse(yInputField.text, out var newY)) {
                y = newY;
            }
            
            SetValue(new Vector2(x, y));
            
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            var value = GetValue();
            xInputField.SetTextWithoutNotify(value.x.ToString("F"));
            yInputField.SetTextWithoutNotify(value.y.ToString("F"));
        }
    }
}