using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using ComputerysModdingUtilities;
using HarmonyLib;
using ModMenu.Utils;
using UnityEngine;

[assembly: StraftatMod(isVanillaCompatible: true)]

namespace ModMenu
{
    public static class PluginInfo
    {
        public const string guid = "kestrel.straftat.modmenu";
        public const string name = "ModMenu";
        public const string version = "0.0.1";
    }

    [BepInPlugin(PluginInfo.guid, PluginInfo.name, PluginInfo.version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }
        internal static new ManualLogSource Logger;
    
        public static readonly string loadBearingColonThree = ":3";
        private void Awake() {
            if (loadBearingColonThree != ":3") Application.Quit();
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Instance = this;
            Logger = base.Logger;
        
            Assets.Init();
            CreateExampleConfigs();
            new Harmony(PluginInfo.guid).PatchAll();
            Logger.LogInfo("Hiiiiiiiiiiii :3");
        }
        
        private void CreateExampleConfigs() {
            Config.Bind("Examples.Basic", "Bool", false, "A bool");
            Config.Bind("Examples.Basic", "String", "Hiiiii", "A string");
            
            Config.Bind("Examples.Numeric", "Unrestricted Float", 0.0f, "A float");
            Config.Bind("Examples.Numeric", "Acceptable Range Float", 0.0f, new ConfigDescription("A float with a range restriction", new AcceptableValueRange<float>(-100f, 100f)));
            Config.Bind("Examples.Numeric", "Acceptable List Float", 0.0f, new ConfigDescription("A float with a list of acceptable values", new AcceptableValueList<float>(0f, 0.5f, 1f, 1.5f, 2f, 10f)));
            Config.Bind("Examples.Numeric", "Unrestricted Int", 0, "An int");
            Config.Bind("Examples.Numeric", "Acceptable Range Int", 0, new ConfigDescription("An int with a range restriction", new AcceptableValueRange<int>(-10, 10)));
            Config.Bind("Examples.Numeric", "Acceptable List Int", 0, new ConfigDescription("An int with a list of acceptable values", new AcceptableValueList<int>(0, 1, 2, 3, 4, 5, 10)));
            Config.Bind("Examples.Numeric", "UInt", (uint)0, "An unsigned int");
            Config.Bind("Examples.Numeric", "SByte", (sbyte)0, "A signed byte");
            Config.Bind("Examples.Numeric", "Byte", (byte)0, "A byte");
            
            Config.Bind("Examples.UnityTypes", "Unrestricted Vector2", Vector2.zero, "A Vector2");
            Config.Bind("Examples.UnityTypes", "Acceptable List Vector2", Vector2.zero, new ConfigDescription(
                "A Vector2 with a list of acceptable values",
                new AcceptableValueList<Vector2>(Vector2.zero, Vector2.one, Vector2.up, Vector2.down, Vector2.left, Vector2.right)
            ));
            Config.Bind("Examples.UnityTypes", "Vector3", Vector3.zero, "A Vector3");
            Config.Bind("Examples.UnityTypes", "Vector4", Vector4.zero, "A Vector4");
            Config.Bind("Examples.UnityTypes", "Quaternion", Quaternion.identity, "A Quaternion");
            Config.Bind("Examples.UnityTypes", "Color", Color.white, "A Color");
            
            Config.Bind("Examples.Other", "Enum", TestEnum.OptionA, "An enum");
            Config.Bind("Examples.Other", "Keyboard Shortcut", new KeyboardShortcut(KeyCode.F4, KeyCode.LeftAlt), "A keyboard shortcut");
            Config.Bind("Examples.Other", "KeyCode", KeyCode.A, "A keycode");
        }

        public enum TestEnum
        {
            OptionA,
            OptionB,
            OptionC,
            OptionD,
            AndSoOn
        }
    }
}