using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class KeyboardShortcutOption : ValueOption<KeyboardShortcut>
    {
        public KeyboardShortcutOption(ConfigEntryBase entry) : base(entry) { }
        
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.KeyboardShortcutOption, parent);
        }
    }
}