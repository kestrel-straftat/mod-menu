using ModMenu.Options;
using TMPro;

namespace ModMenu.Behaviours.OptionControllers
{
    public class StringInputFieldOptionController : ValueOptionController<StringInputFieldOption, string>
    {
        public TMP_InputField inputField;
        
        public void OnInputFieldEndEdit(string value) {
            Option.Value = value;
            UpdateAppearance();
        }

        protected override void OnSetOption() {
            base.OnSetOption();
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            inputField.SetTextWithoutNotify(Option.Value);
        }
    }
}