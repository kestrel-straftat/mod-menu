using System.Linq;
using ModMenu.Mods;
using UnityEngine;

namespace ModMenu.Behaviours
{
    public class ModListPanel : MonoBehaviour
    {
        public GameObject container;
        public GameObject itemPrefab;
        public OptionListPanel optionListPanel;
        public ModInfoPanel modInfoPanel;
        public OptionInfoPanel optionInfoPanel;
        
        private void Awake() {
            // ordering to put mods without icons last
            var ordered = ModMenuManager.mods
                .OrderBy(m => !m.HasAdvancedMetadata)
                .ThenBy(m => m.info.name)
                .ThenBy(m => m.info.guid);
            
            foreach (var mod in ordered) {
                var item = Instantiate(itemPrefab, container.transform).GetComponent<ModListItem>();
                item.name = mod.info.name;
                item.Mod = mod;
                item.OnModSelected += SelectMod;
            }
        }

        private void SelectMod(Mod mod) {
            optionListPanel.ShowListFor(mod);
            optionInfoPanel.ClearInfo();
            modInfoPanel.ShowInfoFor(mod);
        }
    }
}
