using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InnovaFramework.iGUI
{
    public class iDropDown : iObject 
    {
        private Dictionary<string, object> data = new Dictionary<string, object>();
        public List<string> options { get; private set; }
        public int index = 0;
        public object selectedObject
        {
            get
            {
                if(index == -1 || index >= options.Count)
                {
                    return null;
                }

                return data[options[index]];
            }
        }
        public string selectedItem
        {
            get
            {
                if(index == -1 || index >= options.Count)
                {
                    return "";
                }

                return options[index];
            }
        }

        public Action<iObject> OnChanged;

        public iDropDown()
        {
            options = new List<string>();
            size = new Vector2(100, 20);
        }

        public bool AddOption(string option, object value = null)
        {
            if (options.Contains(option)) return false;
            options.Add(option);
            data[option] = value;
            return true;
        }

        public void RemoveOption(string option)
        {
            options.Remove(option);
            if(data.ContainsKey(option))
            {
                data.Remove(option);
            }
        }

        public void RemoveOptionAt(int index)
        {
            if(index < 0 || index >= options.Count)
            {
                return;
            }

            if(data.ContainsKey(options[index]))
            {
                data.Remove(options[index]);
            }

            options.RemoveAt(index);
        }

        public void ClearOption()
        {
            options.Clear();
            data.Clear();
        }

        public override void Render()
        {
            if (style == null)
            {
                style = new GUIStyle(EditorStyles.popup);
            }

            GUI.enabled = enabled;
            EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(rect, index, options.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                if (OnChanged != null)
                {
                    OnChanged(this);
                }
            }
            GUI.enabled = true;
        }
    }
}
