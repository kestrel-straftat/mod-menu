using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class EnumDropdownOption : Option
    {
        public EnumDropdownOption(ConfigEntryBase entry) : base(entry) { }
        
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.EnumDropdownOption, parent);
        }
    }
}