using System;
using System.Linq;
using TMPro;

namespace ModMenu.Behaviours.OptionControllers
{
    public class EnumDropdownOptionController : OptionController
    {
        public TMP_Dropdown dropdown;
        
        //private string[] m_enumNames;
        private Type m_enumType;
        
        public void OnDropdownValueChanged(int index) {
            BaseOption.BoxedValue = index;
            UpdateAppearance();
        }
        protected override void OnSetOption() {
            base.OnSetOption();
            
            m_enumType = BaseOption.BaseEntry.SettingType;
            
            dropdown.ClearOptions();
            dropdown.AddOptions(Enum.GetNames(m_enumType).ToList());
            UpdateAppearance();
        }
        
        public override void UpdateAppearance() {
            dropdown.SetValueWithoutNotify((int)BaseOption.BoxedValue);
        }

    }
}