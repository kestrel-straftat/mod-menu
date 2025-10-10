using ModMenu.Options;
using UnityEngine.UI;

namespace ModMenu.Behaviours.OptionControllers
{
    public class BoolCheckboxOptionController : ValueOptionController<BoolOption, bool>
    {
        public Toggle toggle;

        public void OnCheckboxValueChanged(bool value) {
            SetOptionValue(value);
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            toggle.SetIsOnWithoutNotify(Option.Value);
        }

        protected override void OnOptionAssigned() {
            base.OnOptionAssigned();
            UpdateAppearance();
        }
    }
}