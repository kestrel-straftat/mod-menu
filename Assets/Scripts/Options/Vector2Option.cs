using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class Vector2Option : ValueOption<Vector2>
    {
        public Vector2Option(ConfigEntry<Vector2> entry) : base(entry) { }
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.Vector2Option, parent);
        }
    }
}