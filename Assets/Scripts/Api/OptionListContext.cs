using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Options;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ModMenu.Api
{
    public partial class OptionListContext
    {
        /// <summary>The root transform of the option list</summary>
        public Transform Root { get; }

        private HashSet<GameObject> m_originalChildren = new();
        
        internal OptionListContext(Transform root) {
            Root = root;

            foreach (Transform child in root) {
                m_originalChildren.Add(child.gameObject);
            }
        }

        // helpers for option list generation

        internal GameObject AppendControllerForOption(Option option, OptionInfoPanel infoPanel) {
            var obj = Object.Instantiate(option.GetListItemPrefab(), Root);
            var controller = obj.GetComponent<BoxedValueController>();
            controller.SetupFromOption(option);
            controller.NameText = option.Name;
            controller.OnItemHovered += () => {
                infoPanel.ShowInfoFor(option);
                infoPanel.ShowResetButtonFor(controller);
            };
            return controller.gameObject;
        }
        
        // returns an array containing the children of the root object
        // that have been instantiated since the context was created
        internal GameObject[] GetNewChildren() {
            if (Root.childCount == m_originalChildren.Count) {
                return Array.Empty<GameObject>();
            }

            List<GameObject> newChildren = new();

            for (int i = 0; i < Root.childCount; ++i) {
                var child = Root.GetChild(i).gameObject;
                if (!m_originalChildren.Contains(child)) {
                    newChildren.Add(child);
                }
            }
            
            return newChildren.ToArray();
        }

        private int FindActualIndex(int index) {
            var activeChildren = Root.Cast<Transform>().Where(child => child.gameObject.activeSelf).ToArray();

            index = Math.Clamp(index, 0, activeChildren.Length - 1);

            return activeChildren[index].transform.GetSiblingIndex();
        }
    }
}