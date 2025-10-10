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
                SetOptionValue(KeyboardShortcut.Empty);
                StopRebinding();
                return;
            }
            
            foreach (var key in keysToCheck) {
                if (Input.GetKeyUp(key)) {
                    SetOptionValue(new KeyboardShortcut(key, keysToCheck.Where(Input.GetKey).ToArray()));
                    StopRebinding();
                    break;
                }
            }
        }
    }
}