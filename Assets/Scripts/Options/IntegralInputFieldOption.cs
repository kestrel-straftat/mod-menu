using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

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