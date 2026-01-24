using ModMenu.Options;
using TMPro;

namespace ModMenu.Behaviours.OptionControllers
{
    internal class StringInputFieldOptionController : ValueOptionController<StringInputFieldOption, string>
    {
        public TMP_InputField inputField;
        
        public void OnInputFieldEndEdit(string value) {
            SetOptionValue(value);
            UpdateAppearance();
        }

        protected override void OnOptionAssigned() {
            base.OnOptionAssigned();
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            inputField.SetTextWithoutNotify(Option.Value);
        }
    }
}