using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class StringInputFieldOption : ValueOption<string>
    {
        public StringInputFieldOption(ConfigEntryBase entry) : base(entry) { }
        
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.StringInputFieldOption, parent);
        }
    }
}