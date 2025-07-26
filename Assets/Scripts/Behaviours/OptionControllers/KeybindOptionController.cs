using System.Linq;
using BepInEx;
using ModMenu.Patches;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModMenu.Behaviours.OptionControllers
{
    // base class for shared functionality between KeyCode and KeyboardShortcut option controllers
    public abstract class KeybindOptionController : OptionController
    {
        public TextMeshProUGUI buttonText;
        
        protected bool rebinding;
        protected KeyCode[] keysToCheck;
        
        public void OnRebindButtonPressed() {
            if (!rebinding) {
                StartRebinding();
            }
            else {
                StopRebinding();
            }
            UpdateAppearance();
        }
        
        public override void ResetToDefault() {
            base.ResetToDefault();
            StopRebinding();
        }

        public override void UpdateAppearance() {
            if (rebinding) {
                buttonText.text = "Cancel";
            }
            else {
                buttonText.text = BaseOption.BoxedValue.ToString();
            }
        }
        
        protected abstract void UpdateRebinding();
        
        protected void StartRebinding() {
            rebinding = true;
            PauseManagerPatch.currentlyRebindingContollers.Add(this);
            UpdateAppearance();
        }

        protected void StopRebinding() {
            rebinding = false;
            PauseManagerPatch.currentlyRebindingContollers.Remove(this);
            UpdateAppearance();
        }
        
        protected override void OnSetOption() {
            base.OnSetOption();
            UpdateAppearance();
        }
        
        private void Awake() {
            keysToCheck = UnityInput.Current.SupportedKeyCodes.Except(new[]{ KeyCode.Mouse0, KeyCode.None, KeyCode.Escape }).ToArray();
        }
        
        private void OnDisable() {
            StopRebinding();
        }

        private void Update() {
            if (!rebinding) return;
            
            UpdateRebinding();
        }
    }
}