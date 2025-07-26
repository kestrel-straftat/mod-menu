using ModMenu.Mods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModMenu.Behaviours
{
    public class ModInfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI extraInfoText;
        public Image icon;
        
        private void Awake() {
            ClearInfo();
        }
        
        public void ShowInfoFor(Mod mod) {
            icon.enabled = true;
            nameText.text = mod.info.name;
            descriptionText.text = mod.info.description;
            icon.sprite = mod.info.icon;
            extraInfoText.text = $"{mod.info.guid}\nv{mod.info.version}";
        }

        public void ClearInfo() {
            icon.enabled = false;
            descriptionText.text = "";
            nameText.text = "";
            extraInfoText.text = "";
        }
    }
}