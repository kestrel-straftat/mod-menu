using System;
using System.Collections.Generic;
using ModMenu.Behaviours.OptionControllers;
using ModMenu.Mods;
using ModMenu.Options;
using ModMenu.Utils;
using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours
{
    public class OptionListPanel : MonoBehaviour
    {
        public GameObject container;
        public OptionInfoPanel infoPanel;

		// guid to array of option objects belonging to that mod
        private Dictionary<string, GameObject[]> m_optionCache = new();
        private string m_currentEnabledGuid = "";
        private GameObject m_noOptionsText;
        private GameObject m_noOptionsSadFace;
        
        private void Awake() {
            // these only really need to be created once
            // meaningless microoptimisations with kestrel !!
            m_noOptionsText = CreateHeader("No options found");
            m_noOptionsSadFace = CreateHeader(":c");
            m_noOptionsText.name = "No options found header";
            m_noOptionsSadFace.name = "No options found sad face";
            m_noOptionsSadFace.GetComponent<TextMeshProUGUI>().fontSize = 24;
            m_noOptionsText.SetActive(false);
            m_noOptionsSadFace.SetActive(false);
        }

        public void ShowListFor(Mod mod) {
            ClearList();
            m_currentEnabledGuid = mod.info.guid;

            if (m_optionCache.TryGetValue(m_currentEnabledGuid, out var optionObjects)) {
                foreach (var obj in optionObjects) {
                    obj.SetActive(true);
                    obj.GetComponent<OptionController>()?.UpdateAppearance();
                }
            }
            else {
                // build and cache option list
                var cachedOptions = new List<GameObject>();
                
                if (!mod.HasAnyConfigs) {
                    cachedOptions.Add(m_noOptionsText);
                    cachedOptions.Add(m_noOptionsSadFace);
                    m_noOptionsText.SetActive(true);
                    m_noOptionsSadFace.SetActive(true);
                }
                else {
                    string currentCategory = "";
                    foreach (var option in mod.config) {
                        if (option.Section != currentCategory) {
                            var header = CreateHeader(option.Section);
                            header.name = $"{mod.info.name}/{option.Section}";
                            cachedOptions.Add(header);
                        }

                        currentCategory = option.Section;

                        var optionObject = option.InstantiateOptionObject(container.transform);
                        optionObject.name = $"{mod.info.name}/{option.Section}/{option.Name}";
                        var controller = optionObject.GetComponent<OptionController>();
                        controller.SetOption(option);
                        controller.OnOptionHovered += () => {
                            infoPanel.ShowInfoFor(option);
                            infoPanel.ShowResetButtonFor(controller);
                        };
                        cachedOptions.Add(optionObject);
                    }
                }

                m_optionCache[m_currentEnabledGuid] = cachedOptions.ToArray();
            } 
        }

        private GameObject CreateHeader(string text) {
            var header = Instantiate(Assets.CategoryHeader, container.transform).GetComponent<CategoryHeader>();
            header.Text = text;
            return header.gameObject;
        }

        private void ClearList() {
            if (!m_optionCache.TryGetValue(m_currentEnabledGuid, out var optionObjects)) return;
            foreach (var obj in optionObjects) {
                obj.SetActive(false);
            }
        }
    }
}
