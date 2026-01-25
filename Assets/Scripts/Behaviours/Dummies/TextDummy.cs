using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours.Dummies
{
    public class TextDummy : MonoBehaviour
    {
        public TextMeshProUGUI textMeshProText;

        public string Text {
            get => textMeshProText.text;
            set => textMeshProText.text = value;
        }

        public Color Color {
            get => textMeshProText.color;
            set => textMeshProText.color = value;
        }

        public TMP_Style Style {
            get => textMeshProText.textStyle;
            set => textMeshProText.textStyle = value;
        }

        public float FontSize {
            get => textMeshProText.fontSize;
            set => textMeshProText.fontSize = value;
        }
    }
}