using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class FloatingSliderOption : Option
    {
        public FloatingSliderOption(ConfigEntryBase entry) : base(entry) { }
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.FloatingSliderOption, parent);
        }
    }
}