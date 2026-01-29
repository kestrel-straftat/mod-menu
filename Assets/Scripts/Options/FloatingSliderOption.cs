using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class FloatingSliderOption : Option
    {
        public FloatingSliderOption(ConfigEntryBase entry) : base(entry) { }
        public override GameObject GetListItemPrefab() {
            return Assets.FloatingSliderOption;
        }
    }
}