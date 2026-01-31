using System;
using System.Collections;
using System.Collections.Generic;
using BepInEx.Configuration;
using ModMenu.Behaviours.OptionList;
using ModMenu.Behaviours.OptionList.Dummies;
using ModMenu.Behaviours.OptionList.ValueControllers;
using ModMenu.Utils;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ModMenu.Api
{
    public partial class OptionListContext
    {
        private T PrependItem<T>(T item) where T : OptionListItem {
            item.transform.SetAsFirstSibling();
            return item;
        }

        private T InsertItem<T>(int position, T item) where T : OptionListItem {
            item.transform.SetSiblingIndex(FindActualIndex(position));
            return item;
        }
        
        public TextDummy AppendHeader(string text) {
            var item = Object.Instantiate(Assets.CategoryHeader, Root).GetComponent<TextDummy>();
            item.Text = text;
            return item;
        }
        public TextDummy PrependHeader(string text) => PrependItem(AppendHeader(text));
        public TextDummy InsertHeader(int position, string text) => InsertItem(position, AppendHeader(text));

        public TextDummy AppendTextBox(string text) {
            var item = Object.Instantiate(Assets.TextDummy, Root).GetComponent<TextDummy>();
            item.Text = text;
            return item;
        }
        public TextDummy PrependTextBox(string text) => PrependItem(AppendTextBox(text));
        public TextDummy InsertTextBox(int position, string text) => InsertItem(position, AppendTextBox(text));

        public ButtonDummy AppendButton(string nameText, Action onClick, string buttonText) {
            var item = Object.Instantiate(Assets.ButtonDummy, Root).GetComponent<ButtonDummy>();
            item.button.onClick.AddListener(onClick.Invoke);
            item.ButtonText = buttonText;
            item.NameText = nameText;
            return item;
        }
        public ButtonDummy PrependButton(string nameText, Action onClick, string buttonText) => PrependItem(PrependButton(nameText, onClick, buttonText));
        public ButtonDummy InsertButton(int position, Action onClick, string nameText = "", string buttonText = "") => InsertItem(position, AppendButton(nameText, onClick, buttonText));

        // value controllers
        // this implementation kind of sucks a little but whatever

        private TController AppendValueController<TController, TValue>(GameObject prefab, string nameText, Func<TValue> getter, Action<TValue> setter) where TController : ValueController<TValue> {
            var controller = Object.Instantiate(prefab, Root).GetComponent<TController>();
            controller.SetupFromValues(getter, setter);
            controller.NameText = nameText;
            return controller;
        }
        
        public BoolValueController AppendCheckbox(string nameText, Func<bool> getter, Action<bool> setter)
            => AppendValueController<BoolValueController, bool>(Assets.BoolCheckboxOption, nameText, getter, setter);
        
        public BoolValueController PrependCheckbox(string nameText, Func<bool> getter, Action<bool> setter)
            => PrependItem(AppendCheckbox(nameText, getter, setter));
        
        public BoolValueController InsertCheckbox(int position, string nameText, Func<bool> getter, Action<bool> setter)
            => InsertItem(position, AppendCheckbox(nameText, getter, setter));
        
        
        public StringValueController AppendStringInput(string nameText, Func<string> getter, Action<string> setter)
            => AppendValueController<StringValueController, string>(Assets.StringInputFieldOption, nameText, getter, setter);
        
        public StringValueController PrependStringInput(string nameText, Func<string> getter, Action<string> setter)
            => PrependItem(AppendStringInput(nameText, getter, setter));
        
        public StringValueController InsertStringInput(int position, string nameText, Func<string> getter, Action<string> setter)
            => InsertItem(position, AppendStringInput(nameText, getter, setter));

        
        public ColorValueController AppendColorInput(string nameText, Func<Color> getter, Action<Color> setter)
            => AppendValueController<ColorValueController, Color>(Assets.ColorOption, nameText, getter, setter);
        
        public ColorValueController PrependColorInput(string nameText, Func<Color> getter, Action<Color> setter)
            => PrependItem(AppendColorInput(nameText, getter, setter));
        
        public ColorValueController InsertColorInput(int position, string nameText, Func<Color> getter, Action<Color> setter)
            => InsertItem(position, AppendColorInput(nameText, getter, setter));
        
        
        public KeyboardShortcutValueController AppendKeyboardShortcutInput(string nameText, Func<KeyboardShortcut> getter, Action<KeyboardShortcut> setter)
            => AppendValueController<KeyboardShortcutValueController, KeyboardShortcut>(Assets.KeyboardShortcutOption, nameText, getter, setter);
        
        public KeyboardShortcutValueController PrependKeyboardShortcutInput(string nameText, Func<KeyboardShortcut> getter, Action<KeyboardShortcut> setter)
            => PrependItem(AppendKeyboardShortcutInput(nameText, getter, setter));
        
        public KeyboardShortcutValueController InsertKeyboardShortcutInput(int position, string nameText, Func<KeyboardShortcut> getter, Action<KeyboardShortcut> setter)
            => InsertItem(position, AppendKeyboardShortcutInput(nameText, getter, setter));
        
        
        public KeyCodeValueController AppendKeyCodeInput(string nameText, Func<KeyCode> getter, Action<KeyCode> setter)
            => AppendValueController<KeyCodeValueController, KeyCode>(Assets.KeyCodeOption, nameText, getter, setter);
        
        public KeyCodeValueController PrependKeyCodeInput(string nameText, Func<KeyCode> getter, Action<KeyCode> setter)
            => PrependItem(AppendKeyCodeInput(nameText, getter, setter));
        
        public KeyCodeValueController InsertKeyCodeInput(int position, string nameText, Func<KeyCode> getter, Action<KeyCode> setter)
            => InsertItem(position, AppendKeyCodeInput(nameText, getter, setter));
        
        
        public QuaternionValueController AppendQuaternionInput(string nameText, Func<Quaternion> getter, Action<Quaternion> setter)
            => AppendValueController<QuaternionValueController, Quaternion>(Assets.QuaternionOption, nameText, getter, setter);
        
        public QuaternionValueController PrependQuaternionInput(string nameText, Func<Quaternion> getter, Action<Quaternion> setter)
            => PrependItem(AppendQuaternionInput(nameText, getter, setter));
        
        public QuaternionValueController InsertQuaternionInput(int position, string nameText, Func<Quaternion> getter, Action<Quaternion> setter)
            => InsertItem(position, AppendQuaternionInput(nameText, getter, setter));
        
        
        public Vector2ValueController AppendVector2Input(string nameText, Func<Vector2> getter, Action<Vector2> setter)
            => AppendValueController<Vector2ValueController, Vector2>(Assets.Vector2Option, nameText, getter, setter);
        
        public Vector2ValueController PrependVector2Input(string nameText, Func<Vector2> getter, Action<Vector2> setter)
            => PrependItem(AppendVector2Input(nameText, getter, setter));
        
        public Vector2ValueController InsertVector2Input(int position, string nameText, Func<Vector2> getter, Action<Vector2> setter)
            => InsertItem(position, AppendVector2Input(nameText, getter, setter));
        
        
        public Vector3ValueController AppendVector3Input(string nameText, Func<Vector3> getter, Action<Vector3> setter)
            => AppendValueController<Vector3ValueController, Vector3>(Assets.Vector3Option, nameText, getter, setter);
        
        public Vector3ValueController PrependVector3Input(string nameText, Func<Vector3> getter, Action<Vector3> setter)
            => PrependItem(AppendVector3Input(nameText, getter, setter));
        
        public Vector3ValueController InsertVector3Input(int position, string nameText, Func<Vector3> getter, Action<Vector3> setter)
            => InsertItem(position, AppendVector3Input(nameText, getter, setter));
        
        
        public Vector4ValueController AppendVector4Input(string nameText, Func<Vector4> getter, Action<Vector4> setter)
            => AppendValueController<Vector4ValueController, Vector4>(Assets.Vector4Option, nameText, getter, setter);
        
        public Vector4ValueController PrependVector4Input(string nameText, Func<Vector4> getter, Action<Vector4> setter)
            => PrependItem(AppendVector4Input(nameText, getter, setter));
        
        public Vector4ValueController InsertVector4Input(int position, string nameText, Func<Vector4> getter, Action<Vector4> setter)
            => InsertItem(position, AppendVector4Input(nameText, getter, setter));

        
        public DropdownValueController AppendDropdown<T>(string nameText, Func<T> getter, Action<T> setter) where T : Enum {
            var controller = Object.Instantiate(Assets.EnumDropdownOption, Root).GetComponent<DropdownValueController>();
            controller.SetupFromValues(getter, setter);
            controller.NameText = nameText;
            return controller;
        }
        
        public DropdownValueController PrependDropdown<T>(string nameText, Func<T> getter, Action<T> setter) where T : Enum
            => PrependItem(AppendDropdown(nameText, getter, setter));
        
        public DropdownValueController InsertDropdown<T>(int position, string nameText, Func<T> getter, Action<T> setter) where T : Enum
            => InsertItem(position, AppendDropdown(nameText, getter, setter));


        public NumericInputFieldValueController AppendNumericInputField<T>(string nameText, Func<T> getter, Action<T> setter) 
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable {
            
            var prefab = typeof(T).IsFloating() ? Assets.FloatingInputFieldOption : Assets.IntegralInputFieldOption;
            var controller = Object.Instantiate(prefab, Root).GetComponent<NumericInputFieldValueController>();
            controller.SetupFromValues(getter, setter);
            controller.NameText = nameText;
            return controller;
        }
        
        public NumericInputFieldValueController PrependNumericInputField<T>(string nameText, Func<T> getter, Action<T> setter)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => PrependItem(AppendNumericInputField(nameText, getter, setter));
        
        public NumericInputFieldValueController InsertNumericInputField<T>(int position, string nameText, Func<T> getter, Action<T> setter)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => InsertItem(position, AppendNumericInputField(nameText, getter, setter));

        
        public NumericSliderValueController AppendNumericSlider<T>(string nameText, Func<T> getter, Action<T> setter, float minValue, float maxValue)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable {
            if (minValue >= maxValue) {
                throw new ArgumentException("minValue must be < maxValue");
            }
            var prefab = typeof(T).IsFloating() ? Assets.FloatingSliderOption : Assets.IntegralSliderOption;
            var controller = Object.Instantiate(prefab, Root).GetComponent<NumericSliderValueController>();
            controller.SetupFromValues(getter, setter);
            controller.slider.minValue = minValue;
            controller.slider.maxValue = maxValue;
            controller.NameText = nameText;
            return controller;
        }
        
        public NumericSliderValueController PrependNumericSlider<T>(string nameText, Func<T> getter, Action<T> setter, float minValue, float maxValue)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => PrependItem(AppendNumericSlider(nameText, getter, setter, minValue, maxValue));
        
        public NumericSliderValueController InsertNumericSlider<T>(int position, string nameText, Func<T> getter, Action<T> setter, float minValue, float maxValue)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => InsertItem(position, AppendNumericSlider(nameText, getter, setter, minValue, maxValue));

    }
}