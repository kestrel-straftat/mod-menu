using ModMenu.Mods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModMenu.Behaviours
{
    public class ModListItem : MonoBehaviour
    {
        public Image icon;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI versionText;
        public event ModSelectHandler OnModSelected;
        
        private Mod m_mod;

        public delegate void ModSelectHandler(Mod mod);
        
        public Mod Mod {
            get => m_mod;
            set {
                m_mod = value;
                nameText.text = m_mod.info.name;
                versionText.text = "v"+m_mod.info.version;
                icon.sprite = m_mod.info.icon;
            }
        }

        public void OnClick() {
            OnModSelected?.Invoke(Mod);
        }
    }
}
