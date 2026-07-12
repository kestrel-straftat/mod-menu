using System;
using System.Collections.Generic;
using ModMenu.Behaviours.OptionList;
using UnityEngine;

namespace ModMenu.Api
{
    public partial class OptionListContext
    {
        private OptionListPanel m_panel;
        private OptionInfoPanel m_infoPanel;
        private HashSet<GameObject> m_originalChildren = new();
        
        /// <summary>The root transform of the option list</summary>
        public Transform Root { get; }
        
        internal OptionListContext(OptionListPanel panel) {
            m_panel = panel;
            m_infoPanel = panel.infoPanel;
            Root = panel.container.transform;

            foreach (Transform child in Root) {
                m_originalChildren.Add(child.gameObject);
            }
        }
        
        /// <summary>Sets the contents of the option info panel</summary>
        /// <param name="title">The title to set</param>
        /// <param name="subtitle">The subtitle to set</param>
        /// <param name="description">The description to set</param>
        public void SetInfoPanelContents(string title, string subtitle, string description) {
            m_infoPanel.SetContents(title, subtitle, description);
        }
        
        /// <summary>Clears the contents of the option info panel</summary>
        public void ClearInfoPanelContents() {
            m_infoPanel.ClearInfo();
        }

        /// <summary>Shows the option reset button</summary>
        /// <param name="resetAction">An action that will be invoked when the reset button is clicked</param>
        public void ShowInfoPanelResetButton(Action resetAction) {
            m_infoPanel.resetButton.gameObject.SetActive(true);
            
            m_infoPanel.resetButton.onClick.AddListener(() => resetAction?.Invoke());
        }
    }
}