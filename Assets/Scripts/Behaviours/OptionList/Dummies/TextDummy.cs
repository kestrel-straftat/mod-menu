using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours.OptionList.Dummies
{
    public class TextDummy : OptionListItem
    {
        public string Text {
            get => nameText.text;
            set => nameText.text = value;
        }

        public Color Color {
            get => nameText.color;
            set => nameText.color = value;
        }

        public TMP_Style Style {
            get => nameText.textStyle;
            set => nameText.textStyle = value;
        }

        public float FontSize {
            get => nameText.fontSize;
            set => nameText.fontSize = value;
        }
    }
}