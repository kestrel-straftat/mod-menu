using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using ComputerysModdingUtilities;
using HarmonyLib;
using ModMenu.Api;
using ModMenu.Utils;
using UnityEngine;
using UnityEngine.UI;

[assembly: StraftatMod(isVanillaCompatible: true)]

namespace ModMenu
{
    // ------------------------------------------------------------------
    // VISITORS: See Awake() & the stuff below it for example API usage.
    // ------------------------------------------------------------------
    
    public static class PluginInfo
    {
        public const string guid = "kestrel.straftat.modmenu";
        public const string name = "ModMenu";
        public const string version = "1.1.5";
    }

    [BepInPlugin(PluginInfo.guid, PluginInfo.name, PluginInfo.version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }
        
        internal static new ManualLogSource Logger;
        
        private static ConfigEntry<string> m_superSecretConfigEntry;
    
        public static readonly string loadBearingColonThree = ":3";
        private void Awake() {
            if (loadBearingColonThree != ":3") Application.Quit();
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            Instance = this;
            Logger = base.Logger;
            
            CreateExampleConfigs();
            Assets.Init();

            // register the content builder with the ModMenu API
            ModMenuCustomisation.RegisterContentBuilder(CustomContentBuilder);
            
            // prevent modmenu from generating a ui item for a top secret config entry
            ModMenuCustomisation.HideEntry(m_superSecretConfigEntry);
            
            // Explicitly set description and icon so that they're still shown
            // even when ModMenu can't load them from thunderstore metadata
            ModMenuCustomisation.SetPluginDescription("Configure your mods ingame!");
            ModMenuCustomisation.SetPluginIcon(Assets.ModMenuModIcon);
            
            new Harmony(PluginInfo.guid).PatchAll();
            Logger.LogInfo("Hiiiiiiiiiiii :3");
        }
        
        private enum TestEnum
        {
            OptionA,
            OptionB,
            OptionC,
            OptionD,
            AndSoOn
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

            m_superSecretConfigEntry = Config.Bind("Examples.Secret", "Super Secret Entry", ":3", "shhh");
            m_superSecretConfigEntry.SettingChanged += (_, _) => PauseManager.Instance.WriteOfflineLog(">:3");
        }

        private enum TestEnum2
        {
            AnotherTestEnum,
            IsntThisCool,
            HeyHiiiiHeyLookAtMe,
            ColonThree
        }

        private static void CustomContentBuilder(OptionListContext c) {
            c.InsertHeader(13, "Custom Section 1");
                
            c.InsertTextBox(14, "This section was generated with the Mod Menu API! With it, you can add custom ui elements anywhere in your mod's config page. " +
                                "Go to Assets/Scripts/Plugin.cs in the mod's source code to see example usage of the API.")
                .GetComponent<LayoutElement>().preferredHeight = 128;
            c.InsertButton(15, "", "A very interesting button", () => {
                PauseManager.Instance.WriteOfflineLog("Button pressed!");
            });
            c.InsertButton(16, "Plugins Directory", "Show Path", () => {
                var pluginPath = BepInEx.Paths.PluginPath;
                PauseManager.Instance.WriteOfflineLog($"Plugins directory: {pluginPath}");
            });
            
            c.AppendHeader("Custom Section 2");

            c.AppendTextBox("Another custom section!");
            c.AppendTextBox("With some colored text! :3")
                .Color = new Color(0.77f, 0.53f, 1f);

            var dropdownValue = TestEnum2.AnotherTestEnum;
            c.AppendDropdown("Custom Dropdown", () => dropdownValue, value => {
                dropdownValue = value;
                PauseManager.Instance.WriteOfflineLog($"dropdownValue is {dropdownValue}");
            });
            
            bool boolValue = false;
            c.AppendCheckbox("Custom Checkbox", () => boolValue, value => {
                boolValue = value;
                PauseManager.Instance.WriteOfflineLog($"checkboxValue is {value}");
            });
            
            var colorValue = new Color(0.77f, 0.53f, 1f);
            c.AppendColorInput("Custom Color Input", () => colorValue, value => {
                colorValue = value;
                PauseManager.Instance.WriteOfflineLog($"colorValue is {value}");
            });

            string stringValue = "hiii";
            c.AppendStringInput("Custom String Input", () => stringValue, value => {
                stringValue = value;
                PauseManager.Instance.WriteOfflineLog($"stringValue is {value}");
            });

            var keyboardShortcutValue = KeyboardShortcut.Empty;
            c.AppendKeyboardShortcutInput("Custom Keyboard Shortcut Input", () => keyboardShortcutValue, value => {
                keyboardShortcutValue = value;
                PauseManager.Instance.WriteOfflineLog($"keyboardShortcutValue is {value}");
            });

            var keyCodeValue = KeyCode.None;
            c.AppendKeyCodeInput("Custom KeyCode Input", () => keyCodeValue, value => {
                keyCodeValue = value;
                PauseManager.Instance.WriteOfflineLog($"keyCodeValue is {value}");
            });

            var quaternionValue = Quaternion.identity;
            c.AppendQuaternionInput("Custom Quaternion Input", () => quaternionValue, value => {
                quaternionValue = value;
                PauseManager.Instance.WriteOfflineLog($"quaternionValue is {value}");
            });
            
            var vector2Value = Vector2.zero;
            c.AppendVector2Input("Custom Vector2 Input", () => vector2Value, value => {
                vector2Value = value;
                PauseManager.Instance.WriteOfflineLog($"vector2Value is {value}");
            });
            
            var vector3Value = Vector3.zero;
            c.AppendVector3Input("Custom Vector3 Input", () => vector3Value, value => {
                vector3Value = value;
                PauseManager.Instance.WriteOfflineLog($"vector3Value is {value}");
            });
            
            var vector4Value = Vector4.zero;
            c.AppendVector4Input("Custom Vector4 Input", () => vector4Value, value => {
                vector4Value = value;
                PauseManager.Instance.WriteOfflineLog($"vector4Value is {value}");
            });
            
            c.AppendTextBox("Some weirder stuff")
                .Color = new Color(0.77254903f, 0.5254902f, 1f);
            
            float floatValue = 0;
            
            Action<float> setFloatValue = value => {
                floatValue = value;
                PauseManager.Instance.WriteOfflineLog($"floatValue is {floatValue}");
            };
            
            var inputField = c.AppendNumericInputField("Custom Float Input", () => floatValue, setFloatValue);
            var slider = c.AppendNumericSlider("Custom Float Slider", () => floatValue, setFloatValue, 0, 100);

            // some funky stuff to make the slider & input field visually update correctly when the other is changed
            // beware of this if you have multiple custom items referencing the same value!
            inputField.setter += _ => slider.UpdateAppearance();
            slider.setter += _ => inputField.UpdateAppearance();

            int sliderMin = -200, sliderMax = 200;
            float floatValue2 = 0;

            var floatSlider2 = c.AppendNumericSlider("Custom Float Slider 2", () => floatValue2, value => {
                floatValue2 = value;
                PauseManager.Instance.WriteOfflineLog($"floatValue2 is {floatValue2}");
            }, sliderMin, sliderMax);
            
            var minSlider = c.AppendNumericSlider("Slider 2 Min (int slider example)", () => sliderMin, value => {
                sliderMin = value;
                floatSlider2.slider.minValue = sliderMin;
            }, -1000, sliderMax - 5);

            var maxSlider = c.AppendNumericSlider("Slider 2 Max (int slider example)", () => sliderMax, value => {
                sliderMax = value;
                floatSlider2.slider.maxValue = sliderMax;
            }, sliderMin + 5, 1000);
            
            minSlider.setter += _ => maxSlider.slider.minValue = sliderMin + 5;
            maxSlider.setter += _ => minSlider.slider.maxValue = sliderMax - 5;
        }
    }
}