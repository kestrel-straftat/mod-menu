using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class AcceptableListDropdownOption : Option
    {
        public AcceptableListDropdownOption(ConfigEntryBase entry) : base(entry) { }
        public override GameObject GetListItemPrefab() {
            return Assets.AcceptableListDropdownOption;
        }
    }
}