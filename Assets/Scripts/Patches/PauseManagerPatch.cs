using System.Collections.Generic;
using HarmonyLib;
using ModMenu.Behaviours.OptionControllers;
using ModMenu.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ModMenu.Patches
{
    [HarmonyPatch(typeof(PauseManager))]
    public static class PauseManagerPatch
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void InitModsTab(GameObject ___optionsMenu) {
            if (!___optionsMenu) return;
            // remove 3D tab bar outline
            ___optionsMenu.transform.Find("OPTIONS HUD").GetChild(0).GetChild(3).gameObject.SetActive(false);
            
            
            var pcTab = ___optionsMenu.transform.Find("PcTab").gameObject;
            var audioTab = ___optionsMenu.transform.Find("AudioTab").gameObject;
            var graphTab = ___optionsMenu.transform.Find("GraphTab").gameObject;
            var modsTab = Object.Instantiate(Assets.Load<GameObject>("ModMenu"), ___optionsMenu.transform);
            modsTab.name = "ModsTab";
            modsTab.SetActive(false);
            
            var tabBar = ___optionsMenu.transform.Find("Tabsbutton");
            var modsTabButton = Object.Instantiate(Assets.Load<GameObject>("ModsTabButton"), tabBar.transform).GetComponent<Button>();
            
            // resize all tabs to be 110 across and reposition accordingly
            for (int i = 0; i < tabBar.childCount; ++i) {
                if (tabBar.GetChild(i).GetComponent<RectTransform>() is not { } rect) continue;
                rect.sizeDelta = new Vector2(110, 63.21f);
                rect.anchoredPosition = new Vector2(-710 + i * 110, 343.8f);
                rect.GetComponent<Button>().onClick.AddListener(() => modsTab.SetActive(false));
            }
            
            modsTabButton.onClick.AddListener(() => {
                modsTab.SetActive(true);
                pcTab.SetActive(false);
                audioTab.SetActive(false);
                graphTab.SetActive(false);
            });
        }

        public static readonly HashSet<KeybindOptionController> currentlyRebindingContollers = new();

        // override menu input callback if any keycode/keyboardshortcut options are rebinding
        [HarmonyPatch("Menu")]
        [HarmonyPrefix]
        public static bool DoNotOpenIfRebinding() => currentlyRebindingContollers.Count == 0;
    }
}