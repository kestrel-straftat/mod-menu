using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class StringInputFieldOption : ValueOption<string>
    {
        public StringInputFieldOption(ConfigEntryBase entry) : base(entry) { }
        
        public override GameObject GetListItemPrefab() {
            return Assets.StringInputFieldOption;
        }
    }
}