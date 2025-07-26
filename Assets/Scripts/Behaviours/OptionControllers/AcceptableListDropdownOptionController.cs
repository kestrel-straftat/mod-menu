using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace ModMenu.Behaviours.OptionControllers
{
    // gross reflection: the class
    public class AcceptableListDropdownOptionController : OptionController
    {
        public TMP_Dropdown dropdown;

        private object[] m_optionValues;
        
        public void OnDropdownValueChanged(int index) {
            BaseOption.BoxedValue = m_optionValues[index];
            UpdateAppearance();
        }
        
        public override void UpdateAppearance() {
            dropdown.SetValueWithoutNotify(Array.IndexOf(m_optionValues, BaseOption.BoxedValue));
        }

        protected override void OnSetOption() {
            base.OnSetOption();
            
            var avObject = BaseOption.AcceptableValues;
            var avType = avObject.GetType();
            m_optionValues = ((IEnumerable)avType.GetProperty("AcceptableValues")!.GetValue(avObject)).Cast<object>().ToArray();
            
            dropdown.ClearOptions();
            dropdown.AddOptions(m_optionValues.Select(v => v.ToString()).ToList());
            UpdateAppearance();
        }
    }
}