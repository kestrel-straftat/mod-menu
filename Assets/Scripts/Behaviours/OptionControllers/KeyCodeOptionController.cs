using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using ModMenu.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModMenu.Behaviours.OptionControllers
{
    public class KeyCodeOptionController : ValueOptionController<KeyCodeOption, KeyCode>
    {
        public Button button;
        public TextMeshProUGUI buttonText;

        private bool m_rebinding;
        private KeyCode[] m_keysToCheck;

        public void OnRebindButtonPressed() {
            m_rebinding = !m_rebinding;
            UpdateAppearance();
        }

        private void Awake() {
            m_keysToCheck = UnityInput.Current.SupportedKeyCodes.Except(new[]{ KeyCode.Mouse0, KeyCode.None }).ToArray();
        }
        
        private void OnDisable() {
            m_rebinding = false;
            UpdateAppearance();
        }

        private void Update() {
            if (!m_rebinding) return;

            foreach (var key in m_keysToCheck) {
                if (Input.GetKeyUp(key)) {
                    Option.Value = key;
                    m_rebinding = false;
                    UpdateAppearance();
                    break;
                }
            }
        }

        public override void ResetToDefault() {
            m_rebinding = false;
            base.ResetToDefault();
        }

        protected override void OnSetOption() {
            base.OnSetOption();
            UpdateAppearance();
        }
        
        public override void UpdateAppearance() {
            if (m_rebinding) {
                buttonText.text = "Cancel";
            }
            else {
                buttonText.text = Option.Value.ToString();
            }
        }
    }
}