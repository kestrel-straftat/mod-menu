using System.IO;
using System.Reflection;
using UnityEngine;

namespace ModMenu.Utils
{
    public static class Assets
    {
        private static AssetBundle m_bundle;

        public static Sprite DefaultModIcon { get; private set; }
        public static GameObject CategoryHeader { get; private set; }
        
        public static GameObject IntegralSliderOption { get; private set; }
        public static GameObject FloatingSliderOption { get; private set; }
        public static GameObject IntegralInputFieldOption { get; private set; }
        public static GameObject FloatingInputFieldOption { get; private set; }
        
        public static GameObject BoolCheckboxOption { get; private set; }
        public static GameObject StringInputFieldOption { get; private set; }
        public static GameObject EnumDropdownOption { get; private set; }
        public static GameObject KeyCodeOption { get; private set; }
        public static GameObject KeyboardShortcutOption { get; private set; }
        public static GameObject AcceptableListDropdownOption { get; private set; }
        
        public static GameObject Vector2Option { get; private set; }
        public static GameObject Vector3Option { get; private set; }
        public static GameObject Vector4Option { get; private set; }
        public static GameObject QuaternionOption { get; private set; }
        public static GameObject ColorOption { get; private set; }

        internal static T Load<T>(string name) where T : Object {
            return m_bundle.LoadAsset<T>(name);
        }

        public static void Init() {
            Plugin.Logger.LogInfo("Loading assets...");
            var bundlePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "modmenu");
            m_bundle = AssetBundle.LoadFromFile(bundlePath);
            
            DefaultModIcon = Load<Sprite>("NoIcon");
            CategoryHeader = Load<GameObject>("CategoryHeader");
            
            IntegralSliderOption = Load<GameObject>("IntegralSliderOption");
            FloatingSliderOption = Load<GameObject>("FloatingSliderOption");
            IntegralInputFieldOption = Load<GameObject>("IntegralInputFieldOption");
            FloatingInputFieldOption = Load<GameObject>("FloatingInputFieldOption");
            
            BoolCheckboxOption = Load<GameObject>("BoolCheckboxOption");
            StringInputFieldOption = Load<GameObject>("StringInputFieldOption");
            EnumDropdownOption = Load<GameObject>("EnumDropdownOption");
            KeyCodeOption = Load<GameObject>("KeyCodeOption");
            KeyboardShortcutOption = Load<GameObject>("KeyboardShortcutOption");
            AcceptableListDropdownOption = Load<GameObject>("AcceptableListDropdownOption");
            
            Vector2Option = Load<GameObject>("Vector2Option");
            Vector3Option = Load<GameObject>("Vector3Option");
            Vector4Option = Load<GameObject>("Vector4Option");
            QuaternionOption = Load<GameObject>("QuaternionOption");
            ColorOption = Load<GameObject>("ColorOption");
            
            Plugin.Logger.LogInfo("Assets loaded!");
        }
    }
}