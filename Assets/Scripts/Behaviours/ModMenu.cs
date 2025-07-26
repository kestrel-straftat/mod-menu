using UnityEngine;

namespace ModMenu.Behaviours
{
    public class ModMenu : MonoBehaviour
    {
        public ModListPanel modListPanel;

        private void Awake() {
            ModMenuManager.Init();
        }
    }
}
