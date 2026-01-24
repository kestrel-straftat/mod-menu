using System;
using System.Collections.Generic;
using ModMenu.Api;
using ModMenu.Behaviours.OptionControllers;
using ModMenu.Mods;
using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours
{
    internal class OptionListPanel : MonoBehaviour
    {
        public GameObject container;
        public OptionInfoPanel infoPanel;

		// guid to array of option objects belonging to that mod
        private Dictionary<string, GameObject[]> m_optionCache = new();
        private string m_currentEnabledGuid = "";
        private GameObject m_noOptionsText;
        private GameObject m_noOptionsSadFace;
        
        private void Awake() {
            var context = new OptionListContext(container.transform);
            // these only really need to be created once
            // meaningless microoptimisations with kestrel !!
            m_noOptionsText = context.AppendHeader("No options found").gameObject;
            m_noOptionsSadFace = context.AppendHeader(":c").gameObject;
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
                    var context = new OptionListContext(container.transform);
                    
                    string currentCategory = "";
                    foreach (var option in mod.config) {;
                        if (option.Section != currentCategory) {
                            var header = context.AppendHeader(option.Section);
                            header.name = $"{mod.info.name}/{option.Section}";
                            cachedOptions.Add(header.gameObject);
                        }

                        currentCategory = option.Section;

                        var optionObject = option.InstantiateOptionObject(container.transform);
                        optionObject.name = $"{mod.info.name}/{option.Section}/{option.Name}";
                        var controller = optionObject.GetComponent<OptionController>();
                        controller.BaseOption = option;
                        controller.OnOptionHovered += () => {
                            infoPanel.ShowInfoFor(option);
                            infoPanel.ShowResetButtonFor(controller);
                        };
                        cachedOptions.Add(optionObject);
                    }
                    
                    // refresh context and run user defined builder
                    context = new OptionListContext(container.transform);

                    try {
                        mod.customContentBuilder?.Invoke(context);
                    }
                    catch (Exception e) {
                        Plugin.Logger.LogError($"Error invoking custom content builder for {mod.info.guid}: {e}");
                    }

                    // add all the things the builder made to the cache
                    cachedOptions.AddRange(context.GetNewChildren());
                }

                m_optionCache[m_currentEnabledGuid] = cachedOptions.ToArray();
            } 
        }
        
        private void ClearList() {
            if (!m_optionCache.TryGetValue(m_currentEnabledGuid, out var optionObjects)) return;
            foreach (var obj in optionObjects) {
                obj.SetActive(false);
            }
        }
    }
}
