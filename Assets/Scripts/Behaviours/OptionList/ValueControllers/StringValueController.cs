using TMPro;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public class StringValueController : ValueController<string>
    {
        public TMP_InputField inputField;

        public void OnInputFieldEndEdit(string value) {
            Setter(value);
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            inputField.SetTextWithoutNotify(Getter());
        }
    }
}