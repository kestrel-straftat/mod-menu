using UnityEngine;

namespace ModMenu.Behaviours
{
    internal class ModMenu : MonoBehaviour
    {
        public ModListPanel modListPanel;

        private void Awake() {
            ModMenuManager.Init();
        }
    }
}
