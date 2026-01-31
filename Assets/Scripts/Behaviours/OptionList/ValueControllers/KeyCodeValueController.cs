using UnityEngine;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public class KeyCodeValueController : KeybindValueController<KeyCode>
    {
        protected override void UpdateRebinding() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SetValue(KeyCode.None);
                StopRebinding();
                return;
            }

            foreach (var key in keysToCheck) {
                if (Input.GetKeyUp(key)) {
                    SetValue(key);
                    StopRebinding();
                    break;
                }
            }
        }
    }
}