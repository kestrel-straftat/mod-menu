using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    internal class ColorValueController : ValueController<Color>
    {
        public TMP_InputField inputField;
        public Image preview;
        
        public void OnInputFieldEndEdit(string value) {
            if (ColorUtility.TryParseHtmlString(value, out var color)) {
                Setter(color);
            }

            UpdateAppearance();
        }
        
        public override void UpdateAppearance() {
            inputField.SetTextWithoutNotify("#"+ColorUtility.ToHtmlStringRGBA(Getter()).ToLower());
            preview.color = Getter();
        }
    }
}