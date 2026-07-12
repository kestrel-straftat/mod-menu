using System.Linq;
using ModMenu.Behaviours.OptionList;
using ModMenu.Mods;
using UnityEngine;

namespace ModMenu.Behaviours.ModList
{
    internal class ModListPanel : MonoBehaviour
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
        
        private static bool ShouldDisplayWithFilter(string filter, ModInfo modInfo) {
            string filterLower = filter.ToLower();
            return modInfo.name.ToLower().Contains(filterLower)
                || modInfo.guid.ToLower().Contains(filterLower);
        }

        public void FilterModList(string filter) {
            foreach (Transform child in container.transform) {
                if (filter == string.Empty) {
                    child.gameObject.SetActive(true);
                    continue;
                }
                var item = child.GetComponent<ModListItem>();
                if (item is null) {
                    continue;
                }
                var info = item.Mod.info;
                
                item.gameObject.SetActive(ShouldDisplayWithFilter(filter, info));
            }
        }

        private void SelectMod(Mod mod) {
            optionListPanel.ShowListFor(mod);
            optionInfoPanel.ClearInfo();
            modInfoPanel.ShowInfoFor(mod);
        }
    }
}
