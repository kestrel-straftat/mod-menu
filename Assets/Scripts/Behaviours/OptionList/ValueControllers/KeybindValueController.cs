using System.Linq;
using BepInEx;
using ModMenu.Patches;
using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public abstract class KeybindValueController<T> : ValueController<T>
    {
        public TextMeshProUGUI buttonText;

        protected bool rebinding;
        protected KeyCode[] keysToCheck;

        protected override void Setup() {
            keysToCheck = UnityInput.Current.SupportedKeyCodes.Except(new[] {
                KeyCode.Mouse0,
                KeyCode.None,
                KeyCode.Escape
            }).ToArray();
        }

        public void OnRebindButtonPressed() {
            if (!rebinding) {
                StartRebinding();
            }
            else {
                StopRebinding();
            }
            
            UpdateAppearance();
        }

        public override void ResetValue() {
            base.ResetValue();
            StopRebinding();
        }

        public override void UpdateAppearance() {
            buttonText.text = rebinding ? "Cancel" : Getter().ToString();
        }
        
        protected abstract void UpdateRebinding();

        protected void StartRebinding() {
            rebinding = true;
            PauseManagerPatch.currentlyRebindingKeybindContollers.Add(this);
            UpdateAppearance();
        }

        protected void StopRebinding() {
            rebinding = false;
            PauseManagerPatch.currentlyRebindingKeybindContollers.Remove(this);
            UpdateAppearance();
        }

        private void OnDisable() {
            StopRebinding();
        }

        private void Update() {
            if (rebinding) {
                UpdateRebinding();
            }
        }
    }
}