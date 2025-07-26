using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class Vector4Option : ValueOption<Vector4>
    {
        public Vector4Option(ConfigEntry<Vector4> entry) : base(entry) { }
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.Vector4Option, parent);
        }
    }
}