using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class Vector4Option : ValueOption<Vector4>
    {
        public Vector4Option(ConfigEntryBase entry) : base(entry) { }
        public override GameObject GetListItemPrefab() {
            return Assets.Vector4Option;
        }
    }
}