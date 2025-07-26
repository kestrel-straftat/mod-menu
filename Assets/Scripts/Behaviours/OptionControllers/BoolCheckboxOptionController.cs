using ModMenu.Options;
using UnityEngine.UI;

namespace ModMenu.Behaviours.OptionControllers
{
    public class BoolCheckboxOptionController : ValueOptionController<BoolOption, bool>
    {
        public Toggle toggle;

        public void OnCheckboxValueChanged(bool value) {
            Option.Value = value;
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            toggle.isOn = Option.Value;
        }

        protected override void OnSetOption() {
            base.OnSetOption();
            UpdateAppearance();
        }
    }
}