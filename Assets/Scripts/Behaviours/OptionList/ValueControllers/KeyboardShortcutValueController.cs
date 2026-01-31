using System.Linq;
using BepInEx.Configuration;
using UnityEngine;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public class KeyboardShortcutValueController : KeybindValueController<KeyboardShortcut>
    {
        protected override void UpdateRebinding() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SetValue(KeyboardShortcut.Empty);
                StopRebinding();
                return;
            }

            foreach (var key in keysToCheck) {
                if (Input.GetKeyUp(key)) {
                    SetValue(new KeyboardShortcut(key, keysToCheck.Where(Input.GetKey).ToArray()));
                    StopRebinding();
                    break;
                }
            }
        }
    }
}