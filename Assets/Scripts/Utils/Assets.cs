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
            
            DefaultModIcon = m_bundle.LoadAsset<Sprite>("NoIcon");
            CategoryHeader = m_bundle.LoadAsset<GameObject>("CategoryHeader");
            
            IntegralSliderOption = m_bundle.LoadAsset<GameObject>("IntegralSliderOption");
            FloatingSliderOption = m_bundle.LoadAsset<GameObject>("FloatingSliderOption");
            IntegralInputFieldOption = m_bundle.LoadAsset<GameObject>("IntegralInputFieldOption");
            FloatingInputFieldOption = m_bundle.LoadAsset<GameObject>("FloatingInputFieldOption");
            
            BoolCheckboxOption = m_bundle.LoadAsset<GameObject>("BoolCheckboxOption");
            StringInputFieldOption = m_bundle.LoadAsset<GameObject>("StringInputFieldOption");
            EnumDropdownOption = m_bundle.LoadAsset<GameObject>("EnumDropdownOption");
            KeyCodeOption = m_bundle.LoadAsset<GameObject>("KeyCodeOption");
            KeyboardShortcutOption = m_bundle.LoadAsset<GameObject>("KeyboardShortcutOption");
            AcceptableListDropdownOption = m_bundle.LoadAsset<GameObject>("AcceptableListDropdownOption");
            
            Vector2Option = m_bundle.LoadAsset<GameObject>("Vector2Option");
            Vector3Option = m_bundle.LoadAsset<GameObject>("Vector3Option");
            Vector4Option = m_bundle.LoadAsset<GameObject>("Vector4Option");
            QuaternionOption = m_bundle.LoadAsset<GameObject>("QuaternionOption");
            ColorOption = m_bundle.LoadAsset<GameObject>("ColorOption");
            
            Plugin.Logger.LogInfo("Assets loaded!");
        }
    }
}