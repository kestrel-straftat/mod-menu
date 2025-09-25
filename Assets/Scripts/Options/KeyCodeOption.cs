using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class KeyCodeOption : ValueOption<KeyCode>
    {
        public KeyCodeOption(ConfigEntryBase entry) : base(entry) { }
        
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.KeyCodeOption, parent);
        }
    }
}