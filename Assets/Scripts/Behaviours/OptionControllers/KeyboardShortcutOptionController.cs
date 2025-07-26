using System.Linq;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ModMenu.Behaviours.OptionControllers
{
    public class KeyboardShortcutOptionController : KeybindOptionController
    {
        protected override void UpdateRebinding() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                BaseOption.BoxedValue = KeyboardShortcut.Empty;
                StopRebinding();
                return;
            }
            
            foreach (var key in keysToCheck) {
                if (Input.GetKeyUp(key)) {
                    BaseOption.BoxedValue = new KeyboardShortcut(key, keysToCheck.Where(Input.GetKey).ToArray());
                    StopRebinding();
                    break;
                }
            }
        }
    }
}