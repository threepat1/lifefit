using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InnovaFramework.iGUI
{
    public class iInputField : iObject
    {
        private iInputType inputType;
        public Action<iObject> OnChanged;
        public int intValue;
        public float floatValue;
        public string stringValue;
        public UnityEngine.Object objectValue;
        public Type typeObject;

        public iInputField(iInputType inputType)
        {
            this.inputType = inputType;
            size = new Vector2(400, 20);
            labelSpace = 50;
        }

        public override void Render()
        {
            if (style == null)
            {
                style = new GUIStyle(EditorStyles.textField);
            }

            if(!active) return;

            float labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = text == "" ? 0 : labelSpace;

            GUI.enabled = enabled;

            EditorGUI.BeginChangeCheck();

            switch(inputType)
            {
                case iInputType.INT:
                    {
                        intValue = EditorGUI.IntField(rect, new GUIContent(text, texture, tooltips), intValue, style);
                        break;
                    }
                case iInputType.FLOAT:
                    {
                        floatValue = EditorGUI.FloatField(rect, new GUIContent(text, texture, tooltips), floatValue, style);
                        break;
                    }
                case iInputType.STRING:
                    {
                        stringValue = EditorGUI.TextField(rect, new GUIContent(text, texture, tooltips), stringValue, style);
                        break;
                    }
                case iInputType.OBJECT:
                    {
                        objectValue = EditorGUI.ObjectField(rect, new GUIContent(text, texture, tooltips), objectValue, typeObject, true);
                        break;
                    }
            }

            if(EditorGUI.EndChangeCheck())
            {
                if(OnChanged != null)
                {
                    OnChanged(this);
                }
            }

            GUI.enabled = true;

            EditorGUIUtility.labelWidth = labelWidth;
        }
    }
}

