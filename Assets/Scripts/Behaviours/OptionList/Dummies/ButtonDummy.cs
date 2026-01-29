using TMPro;
using UnityEngine.UI;

namespace ModMenu.Behaviours.OptionList.Dummies
{
    public class ButtonDummy : OptionListItem
    {
        public TextMeshProUGUI buttonTextMeshProText;
        public Button button;
        
        public string ButtonText {
            get => buttonTextMeshProText.text;
            set => buttonTextMeshProText.text = value;
        }

        private void Start() {
            if (!string.IsNullOrWhiteSpace(NameText)) return;
            
            button.GetComponent<LayoutElement>().flexibleWidth = 1.0f;
            nameText.gameObject.SetActive(false);
        }
    }
}