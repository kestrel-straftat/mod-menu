using ModMenu.Options;
using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours.OptionControllers
{
    public class Vector3OptionController : ValueOptionController<Vector3Option, Vector3>
    {
        public TMP_InputField xInputField;
        public TMP_InputField yInputField;
        public TMP_InputField zInputField;
        
        public void OnInputFieldEndEdit(string value) {
            float x = Option.Value.x;
            float y = Option.Value.y;
            float z = Option.Value.z;

            if (float.TryParse(xInputField.text, out var newX)) {
                x = newX;
            }

            if (float.TryParse(yInputField.text, out var newY)) {
                y = newY;
            }
            
            if (float.TryParse(zInputField.text, out var newZ)) {
                z = newZ;
            }
            
            Option.Value = new Vector3(x, y, z);
            
            UpdateAppearance();
        }

        protected override void OnSetOption() {
            base.OnSetOption();
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            xInputField.SetTextWithoutNotify(Option.Value.x.ToString("F"));
            yInputField.SetTextWithoutNotify(Option.Value.y.ToString("F"));
            zInputField.SetTextWithoutNotify(Option.Value.z.ToString("F"));
        }
    }
}