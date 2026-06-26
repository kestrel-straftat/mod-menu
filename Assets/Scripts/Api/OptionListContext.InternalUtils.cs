using System;
using System.Collections.Generic;
using System.Linq;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Options;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ModMenu.Api
{
    public partial class OptionListContext
    {
        // helpers for option list generation

        internal GameObject AppendControllerForOption(Option option) {
            var obj = Object.Instantiate(option.GetListItemPrefab(), Root);
            var controller = obj.GetComponent<BoxedValueController>();
            controller.SetupFromOption(option);
            controller.NameText = option.Name;
            controller.OnItemHovered += () => {
                m_infoPanel.ShowInfoFor(option);
                m_infoPanel.ShowResetButtonFor(controller);
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