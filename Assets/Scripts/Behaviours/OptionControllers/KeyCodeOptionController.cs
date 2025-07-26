using UnityEngine;

namespace ModMenu.Behaviours.OptionControllers
{
    public class KeyCodeOptionController : KeybindOptionController
    {
        protected override void UpdateRebinding() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                BaseOption.BoxedValue = KeyCode.None;
                StopRebinding();
                return;
            }
            
            foreach (var key in keysToCheck) {
                if (Input.GetKeyUp(key)) {
                    BaseOption.BoxedValue = key;
                    StopRebinding();
                    break;
                }
            }
        }
    }
}