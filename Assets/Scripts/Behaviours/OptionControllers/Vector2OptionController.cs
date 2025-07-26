using ModMenu.Options;
using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours.OptionControllers
{
    public class Vector2OptionController : ValueOptionController<Vector2Option, Vector2>
    {
        public TMP_InputField xInputField;
        public TMP_InputField yInputField;
        
        public void OnInputFieldEndEdit(string value) {
            float x = Option.Value.x;
            float y = Option.Value.y;

            if (float.TryParse(xInputField.text, out var newX)) {
                x = newX;
            }

            if (float.TryParse(yInputField.text, out var newY)) {
                y = newY;
            }
            
            Option.Value = new Vector2(x, y);
            
            UpdateAppearance();
        }

        protected override void OnSetOption() {
            base.OnSetOption();
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            xInputField.SetTextWithoutNotify(Option.Value.x.ToString("F"));
            yInputField.SetTextWithoutNotify(Option.Value.y.ToString("F"));
        }
    }
}