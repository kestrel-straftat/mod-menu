using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModMenu.Behaviours.Dummies
{
    public class ButtonDummy : MonoBehaviour
    {
        public TextMeshProUGUI nameTextMeshProText;
        public TextMeshProUGUI buttonTextMeshProText;
        public Button button;
        
        public string NameText {
            get => nameTextMeshProText.text;
            set => nameTextMeshProText.text = value;
        }
        
        public string ButtonText {
            get => buttonTextMeshProText.text;
            set => buttonTextMeshProText.text = value;
        }

        private void Start() {
            if (!string.IsNullOrWhiteSpace(NameText)) return;
            
            var buttonLayout = button.GetComponent<LayoutElement>();
            buttonLayout.flexibleWidth = 1.0f;
            nameTextMeshProText.gameObject.SetActive(false);
        }
    }
}