using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class QuaternionOption : ValueOption<Quaternion>
    {
        public QuaternionOption(ConfigEntryBase entry) : base(entry) { }
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.QuaternionOption, parent);
        }
    }
}