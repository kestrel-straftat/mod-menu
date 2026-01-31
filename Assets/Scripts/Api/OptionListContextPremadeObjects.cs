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
        
        /// <summary>Appends a section header to the option list</summary>
        /// <param name="text">Text to display in the header</param>
        public TextDummy AppendHeader(string text) {
            var item = Object.Instantiate(Assets.CategoryHeader, Root).GetComponent<TextDummy>();
            item.Text = text;
            return item;
        }
        
        /// <summary>Prepends a section header to the option list</summary>
        /// <param name="text">Text to display in the header</param>
        public TextDummy PrependHeader(string text) => PrependItem(AppendHeader(text));
        
        /// <summary>Inserts a section header into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the header will be inserted</param>
        /// <param name="text">Text to display in the header</param>
        public TextDummy InsertHeader(int position, string text) => InsertItem(position, AppendHeader(text));

        
        /// <summary>Appends a text box to the option list</summary>
        /// <param name="text">Text to display in the text box</param>
        public TextDummy AppendTextBox(string text) {
            var item = Object.Instantiate(Assets.TextDummy, Root).GetComponent<TextDummy>();
            item.Text = text;
            return item;
        }
        
        /// <summary>Prepends a text box to the option list</summary>
        /// <param name="text">Text to display in the text box</param>
        public TextDummy PrependTextBox(string text) => PrependItem(AppendTextBox(text));
        
        /// <summary>Inserts a text box into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the text box will be inserted</param>
        /// <param name="text">Text to display in the text box</param>
        public TextDummy InsertTextBox(int position, string text) => InsertItem(position, AppendTextBox(text));

        /// <summary>Appends a button to the option list</summary>
        /// <param name="nameText">Text to display next to the button. The button will fill the line if this is empty.</param>
        /// <param name="onClick">An action that will be invoked when the button is clicked</param>
        /// <param name="buttonText">Text to display on the button</param>
        public ButtonDummy AppendButton(string nameText, string buttonText, Action onClick) {
            var item = Object.Instantiate(Assets.ButtonDummy, Root).GetComponent<ButtonDummy>();
            item.button.onClick.AddListener(onClick.Invoke);
            item.ButtonText = buttonText;
            item.NameText = nameText;
            return item;
        }
        
        /// <summary>Prepends a button to the option list</summary>
        /// <param name="nameText">The text to display next to the button. The button will fill the line if this is empty.</param>
        /// <param name="onClick">An action that will be invoked when the button is clicked</param>
        /// <param name="buttonText">The text to display on the button</param>
        public ButtonDummy PrependButton(string nameText, string buttonText, Action onClick) => PrependItem(PrependButton(nameText, buttonText, onClick));

        /// <summary>Inserts a button into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the button will be inserted</param>
        /// <param name="nameText">The text to display next to the button. The button will fill the line if this is empty.</param>
        /// <param name="onClick">An action that will be invoked when the button is clicked</param>
        /// <param name="buttonText">The text to display on the button</param>
        public ButtonDummy InsertButton(int position, string nameText, string buttonText, Action onClick) => InsertItem(position, AppendButton(nameText, buttonText, onClick));

        // value controllers
        // this implementation kind of sucks a little but whatever

        private TController AppendValueController<TController, TValue>(GameObject prefab, string nameText, Func<TValue> getter, Action<TValue> setter) where TController : ValueController<TValue> {
            var controller = Object.Instantiate(prefab, Root).GetComponent<TController>();
            controller.SetupFromValues(getter, setter);
            controller.NameText = nameText;
            return controller;
        }


        /// <summary>Appends a checkbox to the option list</summary>
        /// <param name="nameText">Text to display next to the checkbox</param>
        /// <param name="getter">A Func returning the value that the checkbox should display</param>
        /// <param name="setter">An Action that is invoked when the checkbox is checked</param>
        public BoolValueController AppendCheckbox(string nameText, Func<bool> getter, Action<bool> setter)
            => AppendValueController<BoolValueController, bool>(Assets.BoolCheckboxOption, nameText, getter, setter);
        
        /// <summary>Prepends a checkbox to the option list</summary>
        /// <param name="nameText">Text to display next to the checkbox</param>
        /// <param name="getter">A Func returning the value that the checkbox should display</param>
        /// <param name="setter">An Action that is invoked when the checkbox is checked</param>
        public BoolValueController PrependCheckbox(string nameText, Func<bool> getter, Action<bool> setter)
            => PrependItem(AppendCheckbox(nameText, getter, setter));

        /// <summary>Inserts a checkbox into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the checkbox will be inserted</param>
        /// <param name="nameText">Text to display next to the checkbox</param>
        /// <param name="getter">A Func returning the value that the checkbox should display</param>
        /// <param name="setter">An Action that is invoked when the checkbox is checked</param>
        public BoolValueController InsertCheckbox(int position, string nameText, Func<bool> getter, Action<bool> setter)
            => InsertItem(position, AppendCheckbox(nameText, getter, setter));
        
        
        /// <summary>Appends a string input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public StringValueController AppendStringInput(string nameText, Func<string> getter, Action<string> setter)
            => AppendValueController<StringValueController, string>(Assets.StringInputFieldOption, nameText, getter, setter);
        
        /// <summary>Prepends a string input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public StringValueController PrependStringInput(string nameText, Func<string> getter, Action<string> setter)
            => PrependItem(AppendStringInput(nameText, getter, setter));
        
        /// <summary>Inserts a string input field into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the input field will be inserted</param>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public StringValueController InsertStringInput(int position, string nameText, Func<string> getter, Action<string> setter)
            => InsertItem(position, AppendStringInput(nameText, getter, setter));

        
        /// <summary>Appends a <see cref="Color"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public ColorValueController AppendColorInput(string nameText, Func<Color> getter, Action<Color> setter)
            => AppendValueController<ColorValueController, Color>(Assets.ColorOption, nameText, getter, setter);
        
        /// <summary>Prepends a <see cref="Color"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public ColorValueController PrependColorInput(string nameText, Func<Color> getter, Action<Color> setter)
            => PrependItem(AppendColorInput(nameText, getter, setter));
        
        /// <summary>Inserts a <see cref="Color"/> input field into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the input field will be inserted</param>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public ColorValueController InsertColorInput(int position, string nameText, Func<Color> getter, Action<Color> setter)
            => InsertItem(position, AppendColorInput(nameText, getter, setter));
        
        
        /// <summary>Appends a <see cref="KeyboardShortcut"/> binding button to the option list</summary>
        /// <param name="nameText">Text to display next to the button</param>
        /// <param name="getter">A Func returning the value that the button should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes rebinding using the button</param>
        public KeyboardShortcutValueController AppendKeyboardShortcutInput(string nameText, Func<KeyboardShortcut> getter, Action<KeyboardShortcut> setter)
            => AppendValueController<KeyboardShortcutValueController, KeyboardShortcut>(Assets.KeyboardShortcutOption, nameText, getter, setter);
        
        /// <summary>Prepends a <see cref="KeyboardShortcut"/> binding button to the option list</summary>
        /// <param name="nameText">Text to display next to the button</param>
        /// <param name="getter">A Func returning the value that the button should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes rebinding using the button</param>
        public KeyboardShortcutValueController PrependKeyboardShortcutInput(string nameText, Func<KeyboardShortcut> getter, Action<KeyboardShortcut> setter)
            => PrependItem(AppendKeyboardShortcutInput(nameText, getter, setter));
        
        /// <summary>Inserts a <see cref="KeyboardShortcut"/> binding button into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the button will be inserted</param>
        /// <param name="nameText">Text to display next to the button</param>
        /// <param name="getter">A Func returning the value that the button should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes rebinding using the button</param>
        public KeyboardShortcutValueController InsertKeyboardShortcutInput(int position, string nameText, Func<KeyboardShortcut> getter, Action<KeyboardShortcut> setter)
            => InsertItem(position, AppendKeyboardShortcutInput(nameText, getter, setter));
        
        
        /// <summary>Appends a <see cref="KeyCode"/> binding button to the option list</summary>
        /// <param name="nameText">Text to display next to the button</param>
        /// <param name="getter">A Func returning the value that the button should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes rebinding a key using the button</param>
        public KeyCodeValueController AppendKeyCodeInput(string nameText, Func<KeyCode> getter, Action<KeyCode> setter)
            => AppendValueController<KeyCodeValueController, KeyCode>(Assets.KeyCodeOption, nameText, getter, setter);
        
        /// <summary>Prepends a <see cref="KeyCode"/> binding button to the option list</summary>
        /// <param name="nameText">Text to display next to the button</param>
        /// <param name="getter">A Func returning the value that the button should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes rebinding a key using the button</param>
        public KeyCodeValueController PrependKeyCodeInput(string nameText, Func<KeyCode> getter, Action<KeyCode> setter)
            => PrependItem(AppendKeyCodeInput(nameText, getter, setter));
        
        /// <summary>Inserts a <see cref="KeyCode"/> binding button into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the button will be inserted</param>
        /// <param name="nameText">Text to display next to the button</param>
        /// <param name="getter">A Func returning the value that the button should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes rebinding using the button</param>
        public KeyCodeValueController InsertKeyCodeInput(int position, string nameText, Func<KeyCode> getter, Action<KeyCode> setter)
            => InsertItem(position, AppendKeyCodeInput(nameText, getter, setter));
        
        
        /// <summary>Appends a <see cref="Quaternion"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public QuaternionValueController AppendQuaternionInput(string nameText, Func<Quaternion> getter, Action<Quaternion> setter)
            => AppendValueController<QuaternionValueController, Quaternion>(Assets.QuaternionOption, nameText, getter, setter);
        
        /// <summary>Prepends a <see cref="Quaternion"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public QuaternionValueController PrependQuaternionInput(string nameText, Func<Quaternion> getter, Action<Quaternion> setter)
            => PrependItem(AppendQuaternionInput(nameText, getter, setter));
        
        /// <summary>Inserts a <see cref="Quaternion"/> input field into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the input field will be inserted</param>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public QuaternionValueController InsertQuaternionInput(int position, string nameText, Func<Quaternion> getter, Action<Quaternion> setter)
            => InsertItem(position, AppendQuaternionInput(nameText, getter, setter));
        
        
        /// <summary>Appends a <see cref="Vector2"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public Vector2ValueController AppendVector2Input(string nameText, Func<Vector2> getter, Action<Vector2> setter)
            => AppendValueController<Vector2ValueController, Vector2>(Assets.Vector2Option, nameText, getter, setter);
        
        /// <summary>Prepends a <see cref="Vector2"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public Vector2ValueController PrependVector2Input(string nameText, Func<Vector2> getter, Action<Vector2> setter)
            => PrependItem(AppendVector2Input(nameText, getter, setter));
        
        /// <summary>Inserts a <see cref="Vector2"/> input field into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the input field will be inserted</param>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public Vector2ValueController InsertVector2Input(int position, string nameText, Func<Vector2> getter, Action<Vector2> setter)
            => InsertItem(position, AppendVector2Input(nameText, getter, setter));
        
        /// <summary>Appends a <see cref="Vector3"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public Vector3ValueController AppendVector3Input(string nameText, Func<Vector3> getter, Action<Vector3> setter)
            => AppendValueController<Vector3ValueController, Vector3>(Assets.Vector3Option, nameText, getter, setter);
        
        /// <summary>Prepends a <see cref="Vector3"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public Vector3ValueController PrependVector3Input(string nameText, Func<Vector3> getter, Action<Vector3> setter)
            => PrependItem(AppendVector3Input(nameText, getter, setter));
        
        /// <summary>Inserts a <see cref="Vector3"/> input field into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the input field will be inserted</param>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public Vector3ValueController InsertVector3Input(int position, string nameText, Func<Vector3> getter, Action<Vector3> setter)
            => InsertItem(position, AppendVector3Input(nameText, getter, setter));
        
        /// <summary>Appends a <see cref="Vector4"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public Vector4ValueController AppendVector4Input(string nameText, Func<Vector4> getter, Action<Vector4> setter)
            => AppendValueController<Vector4ValueController, Vector4>(Assets.Vector4Option, nameText, getter, setter);
        
        /// <summary>Prepends a <see cref="Vector4"/> input field to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public Vector4ValueController PrependVector4Input(string nameText, Func<Vector4> getter, Action<Vector4> setter)
            => PrependItem(AppendVector4Input(nameText, getter, setter));
        
        /// <summary>Inserts a <see cref="Vector4"/> input field into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the input field will be inserted</param>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public Vector4ValueController InsertVector4Input(int position, string nameText, Func<Vector4> getter, Action<Vector4> setter)
            => InsertItem(position, AppendVector4Input(nameText, getter, setter));

        
        /// <summary>Appends a dropdown representing an enum to the option list</summary>
        /// <param name="nameText">Text to display next to the dropdown</param>
        /// <param name="getter">A Func returning the value that the dropdown should display</param>
        /// <param name="setter">An Action that is invoked when a user selects an item from the dropdown</param>
        public DropdownValueController AppendDropdown<T>(string nameText, Func<T> getter, Action<T> setter) where T : Enum {
            var controller = Object.Instantiate(Assets.EnumDropdownOption, Root).GetComponent<DropdownValueController>();
            controller.SetupFromValues(getter, setter);
            controller.NameText = nameText;
            return controller;
        }
        
        /// <summary>Prepends a dropdown representing an enum to the option list</summary>
        /// <param name="nameText">Text to display next to the dropdown</param>
        /// <param name="getter">A Func returning the value that the dropdown should display</param>
        /// <param name="setter">An Action that is invoked when a user selects an item from the dropdown</param>
        public DropdownValueController PrependDropdown<T>(string nameText, Func<T> getter, Action<T> setter) where T : Enum
            => PrependItem(AppendDropdown(nameText, getter, setter));

        /// <summary>Inserts a dropdown representing an enum into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the dropdown will be inserted</param>
        /// <param name="nameText">Text to display next to the dropdown</param>
        /// <param name="getter">A Func returning the value that the dropdown should display</param>
        /// <param name="setter">An Action that is invoked when a user selects an item from the dropdown</param>
        public DropdownValueController InsertDropdown<T>(int position, string nameText, Func<T> getter, Action<T> setter) where T : Enum
            => InsertItem(position, AppendDropdown(nameText, getter, setter));


        /// <summary>Appends an input field supporting any numeric type to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public NumericInputFieldValueController AppendNumericInputField<T>(string nameText, Func<T> getter, Action<T> setter) 
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable {
            
            var prefab = typeof(T).IsFloating() ? Assets.FloatingInputFieldOption : Assets.IntegralInputFieldOption;
            var controller = Object.Instantiate(prefab, Root).GetComponent<NumericInputFieldValueController>();
            controller.SetupFromValues(getter, setter);
            controller.NameText = nameText;
            return controller;
        }
        
        /// <summary>Prepends an input field supporting any numeric type to the option list</summary>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public NumericInputFieldValueController PrependNumericInputField<T>(string nameText, Func<T> getter, Action<T> setter)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => PrependItem(AppendNumericInputField(nameText, getter, setter));
        
        /// <summary>Inserts an input field supporting any numeric type into the option list, at the specified position</summary>
        /// <param name="position">The index of the option list at which the input field will be inserted</param>
        /// <param name="nameText">Text to display next to the input field</param>
        /// <param name="getter">A Func returning the value that the input field should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the input field</param>
        public NumericInputFieldValueController InsertNumericInputField<T>(int position, string nameText, Func<T> getter, Action<T> setter)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => InsertItem(position, AppendNumericInputField(nameText, getter, setter));


        /// <summary>Appends a slider supporting any numeric type to the option list</summary>
        /// <param name="nameText">Text to display next to the slider</param>
        /// <param name="getter">A Func returning the value that the slider should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the slider</param>
        /// <param name="minValue">The minimum value of the slider</param>
        /// <param name="maxValue">The maximum value of the slider</param>
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
        
        /// <summary>Prepends a slider supporting any numeric type to the option list</summary>
        /// <param name="nameText">Text to display next to the slider</param>
        /// <param name="getter">A Func returning the value that the slider should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the slider</param>
        /// <param name="minValue">The minimum value of the slider</param>
        /// <param name="maxValue">The maximum value of the slider</param>
        public NumericSliderValueController PrependNumericSlider<T>(string nameText, Func<T> getter, Action<T> setter, float minValue, float maxValue)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => PrependItem(AppendNumericSlider(nameText, getter, setter, minValue, maxValue));
        
        /// <summary>Inserts a slider supporting any numeric type into the option list</summary>
        /// <param name="position">The index of the option list at which the slider will be inserted</param>
        /// <param name="nameText">Text to display next to the slider</param>
        /// <param name="getter">A Func returning the value that the slider should display</param>
        /// <param name="setter">An Action that is invoked when a user finishes editing the text in the slider</param>
        /// <param name="minValue">The minimum value of the slider</param>
        /// <param name="maxValue">The maximum value of the slider</param>
        public NumericSliderValueController InsertNumericSlider<T>(int position, string nameText, Func<T> getter, Action<T> setter, float minValue, float maxValue)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            => InsertItem(position, AppendNumericSlider(nameText, getter, setter, minValue, maxValue));

    }
}