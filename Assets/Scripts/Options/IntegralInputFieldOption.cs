using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class IntegralInputFieldOption : Option
    {
        public IntegralInputFieldOption(ConfigEntryBase entry) : base(entry) { }
        public override GameObject GetListItemPrefab() {
            return Assets.IntegralInputFieldOption;
        }
    }
}