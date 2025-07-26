using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class ColorOption : ValueOption<Color>
    {
        public ColorOption(ConfigEntry<Color> entry) : base(entry) { }
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.ColorOption, parent);
        }
    }
}