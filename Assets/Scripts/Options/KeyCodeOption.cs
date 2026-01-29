using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class KeyCodeOption : ValueOption<KeyCode>
    {
        public KeyCodeOption(ConfigEntryBase entry) : base(entry) { }
        
        public override GameObject GetListItemPrefab() {
            return Assets.KeyCodeOption;
        }
    }
}