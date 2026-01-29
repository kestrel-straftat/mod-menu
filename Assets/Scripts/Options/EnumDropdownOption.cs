using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class EnumDropdownOption : Option
    {
        public EnumDropdownOption(ConfigEntryBase entry) : base(entry) { }
        
        public override GameObject GetListItemPrefab() {
            return Assets.EnumDropdownOption;
        }
    }
}