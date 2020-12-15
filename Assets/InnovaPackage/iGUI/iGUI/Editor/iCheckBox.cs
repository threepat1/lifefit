using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InnovaFramework.iGUI
{
    public class iCheckBox : iObject
    {
        public bool isChecked = false;
        public Action<iObject> OnChanged;
        public iCheckBox()
        {
            size = new Vector2(64,24);
        }

        public override void Render()
        {
            if (style == null)
            {
                style = new GUIStyle(EditorStyles.toggle);
            }

            if(!active) return;

            GUI.enabled = enabled;
            EditorGUI.BeginChangeCheck();
            isChecked = GUI.Toggle(rect, isChecked, new GUIContent(text, texture, tooltips), style);
            if(EditorGUI.EndChangeCheck())
            {
                if(OnChanged != null)
                {
                    OnChanged(this);
                }
            }
            GUI.enabled = true;
        }
    }
}
