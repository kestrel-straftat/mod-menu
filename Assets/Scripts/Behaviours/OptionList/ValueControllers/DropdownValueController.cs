using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using ModMenu.Options;
using ModMenu.Utils;
using TMPro;

namespace ModMenu.Behaviours.OptionList.ValueControllers
{
    public class DropdownValueController : BoxedValueController
    {
        public TMP_Dropdown dropdown;

        private string[] m_dropdownNames;
        private object[] m_dropdownValues;

        protected override void Setup() {
            dropdown.ClearOptions();
            dropdown.AddOptions(m_dropdownNames.ToList());
        }

        public void OnDropdownValueChanged(int index) {
            setter.Invoke(m_dropdownValues[index]);
            UpdateAppearance();
        }

        public override void UpdateAppearance() {
            dropdown.SetValueWithoutNotify(Array.IndexOf(m_dropdownValues, getter.Invoke()));
        }

        internal override void SetupFromOption(Option option) {
            var settingType = option.BaseEntry.SettingType;
            if (settingType.IsEnum) {
                SetValuesFromEnum(settingType);
            }
            else {
                if (!option.AcceptableValues.IsAcceptableValueList()) {
                    throw new InvalidOperationException("Cannot setup a dropdown value controller from a non-enum option type with no acceptable value list.");
                }
                SetValuesFromOption(option);
            }
            base.SetupFromOption(option);
        }

        internal override void SetupFromValues<T>(Func<T> getter, Action<T> setter) {
            if (!typeof(T).IsEnum) {
                throw new InvalidOperationException("Cannot setup a dropdown value controller by values from a non-enum type");
            }
            
            SetValuesFromEnum(typeof(T));
            
            base.SetupFromValues(getter, setter);
        }

        private void SetValuesFromEnum(Type enumType) {
            m_dropdownNames = Enum.GetNames(enumType);
            m_dropdownValues = Enum.GetValues(enumType).Cast<object>().ToArray();
        }

        private void SetValuesFromOption(Option option) {
            var acceptableValueBase = option.AcceptableValues;
            m_dropdownValues = ((IEnumerable)acceptableValueBase.GetType().GetProperty("AcceptableValues")!.GetValue(acceptableValueBase)).Cast<object>().ToArray();
            m_dropdownNames = m_dropdownValues.Select(v => v.ToString()).ToArray();
        }
    }
}