using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public class Vector3Option : ValueOption<Vector3>
    {
        public Vector3Option(ConfigEntry<Vector3> entry) : base(entry) { }
        public override GameObject InstantiateOptionObject(Transform parent) {
            return Object.Instantiate(Assets.Vector3Option, parent);
        }
    }
}