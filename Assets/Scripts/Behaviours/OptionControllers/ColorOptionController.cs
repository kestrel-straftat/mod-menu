using ModMenu.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModMenu.Behaviours.OptionControllers
{
    internal class ColorOptionController : ValueOptionController<ColorOption, Color>
    {
        public TMP_InputField inputField;
        public Image preview;
        
        public void OnInputFieldEndEdit(string value) {
            if (ColorUtility.TryParseHtmlString(value, out var color)) {
                SetOptionValue(color);
            }

            UpdateAppearance();
        }

        protected override void OnOptionAssigned() {
            base.OnOptionAssigned();
            UpdateAppearance();
        }
        
        public override void UpdateAppearance() {
            inputField.SetTextWithoutNotify("#"+ColorUtility.ToHtmlStringRGBA(Option.Value).ToLower());
            preview.color = Option.Value;
        }
    }
}