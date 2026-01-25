using System;
using System.Collections.Generic;
using System.Linq;
using ModMenu.Behaviours;
using ModMenu.Behaviours.Dummies;
using ModMenu.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ModMenu.Api
{
    public class OptionListContext
    {
        public Transform Root { get; }

        private HashSet<GameObject> originalChildren = new();
        
        public OptionListContext(Transform root) {
            Root = root;

            foreach (Transform child in root) {
                originalChildren.Add(child.gameObject);
            }
        }
        
        // premade objects
        
        public TextDummy AppendHeader(string text) {
            var dummy = Object.Instantiate(Assets.CategoryHeader, Root).GetComponent<TextDummy>();
            dummy.Text = text;
            return dummy;
        }

        public TextDummy PrependHeader(string text) {
            var dummy = AppendHeader(text);
            dummy.transform.SetAsFirstSibling();
            return dummy;
        }
        
        public TextDummy InsertHeader(int position, string text) {
            var dummy = AppendHeader(text);
            dummy.transform.SetSiblingIndex(FindActualIndex(position));
            return dummy;
        }

        public TextDummy AppendTextBox(string text) {
            var dummy = Object.Instantiate(Assets.TextDummy, Root).GetComponent<TextDummy>();
            dummy.Text = text;
            return dummy;
        }

        public TextDummy PrependTextBox(string text) {
            var dummy = AppendTextBox(text);
            dummy.transform.SetAsLastSibling();
            return dummy;
        }

        public TextDummy InsertTextBox(int position, string text) {
            var dummy = AppendTextBox(text);
            dummy.transform.SetSiblingIndex(FindActualIndex(position));
            return dummy;
        }

        public ButtonDummy AppendButton(Action onClick, string buttonText, string nameText = "") {
            var dummy = Object.Instantiate(Assets.ButtonDummy, Root).GetComponent<ButtonDummy>();
            dummy.button.onClick.AddListener(onClick.Invoke);
            dummy.ButtonText = buttonText;
            dummy.NameText = nameText;
            return dummy;
        }

        public ButtonDummy PrependButton(Action onClick, string buttonText, string nameText = "") {
            var dummy = AppendButton(onClick, buttonText, nameText);
            dummy.transform.SetAsLastSibling();
            return dummy;
        }

        public ButtonDummy InsertButton(int position, Action onClick, string nameText = "", string buttonText = "") {
            var dummy = AppendButton(onClick, buttonText, nameText);
            dummy.transform.SetSiblingIndex(FindActualIndex(position));
            return dummy;
        }
        
        // helpers for option list generation

        // returns an array containing the children of the root object
        // that have been instantiated since the context was created
        internal GameObject[] GetNewChildren() {
            if (Root.childCount == originalChildren.Count) {
                return Array.Empty<GameObject>();
            }

            List<GameObject> newChildren = new();

            for (int i = 0; i < Root.childCount; ++i) {
                var child = Root.GetChild(i).gameObject;
                if (!originalChildren.Contains(child)) {
                    newChildren.Add(child);
                }
            }
            
            return newChildren.ToArray();
        }

        private int FindActualIndex(int index) {
            var activeChildren = Root.Cast<Transform>().Where(child => child.gameObject.activeSelf).ToArray();

            index = Math.Clamp(index, 0, activeChildren.Length - 1);

            return activeChildren[index].transform.GetSiblingIndex();
        }
    }
}