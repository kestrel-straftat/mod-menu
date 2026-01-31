using UnityEngine.UI;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public class BoolValueController : ValueController<bool>
    {
        public Toggle toggle;

        public void OnCheckboxValueChanged(bool value) {
            SetValue(value);
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            toggle.SetIsOnWithoutNotify(GetValue());
        }
    }
}