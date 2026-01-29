using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class BoolOption : ValueOption<bool>
    {
        public BoolOption(ConfigEntryBase entry) : base(entry) { }
        
        public override GameObject GetListItemPrefab() {
            return Assets.BoolCheckboxOption;
        }
    }
}