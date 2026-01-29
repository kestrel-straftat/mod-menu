using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    internal class Vector2Option : ValueOption<Vector2>
    {
        public Vector2Option(ConfigEntryBase entry) : base(entry) { }
        public override GameObject GetListItemPrefab() {
            return Assets.Vector2Option;
        }
    }
}