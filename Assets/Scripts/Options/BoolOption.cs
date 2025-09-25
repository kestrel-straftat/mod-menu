using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class BoolOption : ValueOption<bool>
    {
        public BoolOption(ConfigEntryBase entry) : base(entry) { }
        
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.BoolCheckboxOption, parent);
        }
    }
}