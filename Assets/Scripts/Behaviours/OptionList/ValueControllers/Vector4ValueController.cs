using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public class Vector4ValueController : ValueController<Vector4>
    {
        public TMP_InputField xInputField;
        public TMP_InputField yInputField;
        public TMP_InputField zInputField;
        public TMP_InputField wInputField;
        
        public void OnInputFieldEndEdit() {
            var originalValue = Getter();
            float x = originalValue.x;
            float y = originalValue.y;
            float z = originalValue.z;
            float w = originalValue.w;

            if (float.TryParse(xInputField.text, out var newX)) {
                x = newX;
            }

            if (float.TryParse(yInputField.text, out var newY)) {
                y = newY;
            }
            
            if (float.TryParse(zInputField.text, out var newZ)) {
                z = newZ;
            }

            if (float.TryParse(wInputField.text, out var newW)) {
                w = newW;
            }
            
            Setter(new Vector4(x, y, z, w));
            
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            var value = Getter();
            xInputField.SetTextWithoutNotify(value.x.ToString("F"));
            yInputField.SetTextWithoutNotify(value.y.ToString("F"));
            zInputField.SetTextWithoutNotify(value.z.ToString("F"));
            wInputField.SetTextWithoutNotify(value.w.ToString("F"));
        }
    }
}