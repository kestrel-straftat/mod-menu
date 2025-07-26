using TMPro;
using UnityEngine;

namespace ModMenu.Behaviours
{
    public class CategoryHeader : MonoBehaviour
    {
        public TextMeshProUGUI headerText;
        
        public string Text {
            get => headerText.text;
            set => headerText.text = value;
        }
    }
}