using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ModMenu.Options
{
    public class IntegralInputFieldOption : Option
    {
        public IntegralInputFieldOption(ConfigEntryBase entry) : base(entry) { }
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.IntegralInputFieldOption, parent);
        }
    }
}