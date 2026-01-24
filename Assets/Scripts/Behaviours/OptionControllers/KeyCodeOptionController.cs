using UnityEngine;

namespace ModMenu.Behaviours.OptionControllers
{
    internal class KeyCodeOptionController : KeybindOptionController
    {
        protected override void UpdateRebinding() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SetOptionValue(KeyCode.None);
                StopRebinding();
                return;
            }
            
            foreach (var key in keysToCheck) {
                if (Input.GetKeyUp(key)) {
                    SetOptionValue(key);
                    StopRebinding();
                    break;
                }
            }
        }
    }
}