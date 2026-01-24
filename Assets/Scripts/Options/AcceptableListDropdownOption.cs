using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class AcceptableListDropdownOption : Option
    {
        public AcceptableListDropdownOption(ConfigEntryBase entry) : base(entry) { }
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.AcceptableListDropdownOption, parent);
        }
    }
}