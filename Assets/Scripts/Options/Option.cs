using System;
using BepInEx.Configuration;
using ModMenu.Utils;
using UnityEngine;

namespace ModMenu.Options
{
    public abstract class Option
    {
        protected Option(ConfigEntryBase entry) {
            BaseEntry = entry;
        }
        
        public string Name => BaseEntry.Definition.Key;
        public string Section => BaseEntry.Definition.Section;
        public string Description => BaseEntry.Description.Description;
        public AcceptableValueBase AcceptableValues => BaseEntry.Description.AcceptableValues;
        
        public ConfigEntryBase BaseEntry { get; }
        
        public object BoxedValue {
            get => BaseEntry.BoxedValue;
            set => BaseEntry.BoxedValue = value;
        }
        
        public abstract GameObject InstantiateOptionObject(Transform parent);
        
        public void ResetToDefault() {
            BoxedValue = BaseEntry.DefaultValue;
        }

        // if statements !!!!!!!!!
        public static Option CreateForEntry(ConfigEntryBase entry) {
            var type = entry.SettingType;
            var acceptableValues = entry.Description.AcceptableValues;
            
            // dropdown for lists of defined values
            if (acceptableValues is not null && acceptableValues.IsAcceptableValueList()) {
                return new AcceptableListDropdownOption(entry);
            }
            if (type == typeof(bool)) {
                return new BoolOption(entry);
            }
            if (type == typeof(string)) {
                return new StringInputFieldOption(entry);
            }
            
            // catch-all for numeric types
            // (any explicit definitions for numeric types must come *before* this for obvious reasons)
            if (type.IsIntegral()) {
                if (acceptableValues is not null && acceptableValues.IsAcceptableValueRange()) {
                    return new IntegralSliderOption(entry);
                }
                return new IntegralInputFieldOption(entry);
            } else if (type.IsFloating()) {
                if (acceptableValues is not null && acceptableValues.IsAcceptableValueRange()) {
                    return new FloatingSliderOption(entry);
                }
                return new FloatingInputFieldOption(entry);
            }
            
            if (type == typeof(KeyCode)) {
                return new KeyCodeOption(entry);
            }
            if (type == typeof(KeyboardShortcut)) {
                return new KeyboardShortcutOption(entry);
            }
            if (type == typeof(Vector2)) {
                return new Vector2Option(entry);
            }
            if (type == typeof(Vector3)) {
                return new Vector3Option(entry);
            }
            if (type == typeof(Vector4)) {
                return new Vector4Option(entry);
            }
            if (type == typeof(Quaternion)) {
                return new QuaternionOption(entry);
            }
            if (type == typeof(Color)) {
                return new ColorOption(entry);
            }
            // enum dropdown is not suitable for flags enums
            if (type.IsEnum && !type.IsDefined(typeof(FlagsAttribute), false)) {
                return new EnumDropdownOption(entry);
            }
            
            throw new NotSupportedException($"Options of type \"{type}\" are not supported.");
        }
    }

    public abstract class ValueOption<T> : Option
    {
        protected ValueOption(ConfigEntryBase entry) : base(entry) { }
        
        public T Value {
            get => (T) BaseEntry.BoxedValue;
            set => BaseEntry.BoxedValue = value;
        }
    }
}