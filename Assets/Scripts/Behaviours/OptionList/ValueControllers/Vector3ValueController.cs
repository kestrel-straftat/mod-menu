using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    internal class Vector3ValueController : ValueController<Vector3>
    {
        public TMP_InputField xInputField;
        public TMP_InputField yInputField;
        public TMP_InputField zInputField;
        
        public void OnInputFieldEndEdit(string value) {
            var originalValue = GetValue();
            float x = originalValue.x;
            float y = originalValue.y;
            float z = originalValue.z;

            if (float.TryParse(xInputField.text, out var newX)) {
                x = newX;
            }

            if (float.TryParse(yInputField.text, out var newY)) {
                y = newY;
            }
            
            if (float.TryParse(zInputField.text, out var newZ)) {
                z = newZ;
            }
            
            SetValue(new Vector3(x, y, z));
            
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            var value = GetValue();
            xInputField.SetTextWithoutNotify(value.x.ToString("F"));
            yInputField.SetTextWithoutNotify(value.y.ToString("F"));
            zInputField.SetTextWithoutNotify(value.z.ToString("F"));
        }
    }
}