using System;
using BepInEx.Configuration;
using ModMenu.Behaviours.OptionControllers;
using ModMenu.Extensions;
using ModMenu.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModMenu.Behaviours
{
    public class OptionInfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI categoryText;
        public TextMeshProUGUI descriptionText;
        public Button resetButton;

        private void Awake() {
            ClearInfo();
        }
        
        // RemoveAllListeners removes the ButtonSizeTween listener responsible
        // for click sounds ~ so this is assigned to the reset button as a persistent
        // call as a workaround.
        public void PlayClickSound() {
            PauseManager.Instance.PlayMenuClip(PauseManager.Instance.releaseMenuClip);
        }

        public void ShowInfoFor(Option option) {
            ClearInfo();
            nameText.text = option.Name;
            descriptionText.text = option.Description;

            string categoryName = option.Section;
            string typeName = option.BaseEntry.SettingType.Name;

            categoryText.text = categoryName + " | " + typeName + " | Defaults to " + option.BaseEntry.DefaultValue;

            if (option.AcceptableValues is not null) {
                categoryText.text += " | " + option.AcceptableValues.HumanizedString();
            }
        }

        public void ShowResetButtonFor(OptionController controller) {
            resetButton.gameObject.SetActive(true);
            
            resetButton.onClick.AddListener(() => {
                controller.ResetToDefault();
                controller.UpdateAppearance();
            });
        }

        
        public void ClearInfo() {
            resetButton.onClick.RemoveAllListeners();
            resetButton.gameObject.SetActive(false);
            nameText.text = "";
            categoryText.text = "";
            descriptionText.text = "";
        }
    }
}