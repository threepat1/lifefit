using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

namespace InnovaFramework.iGUI
{
    public class iButton : iObject
    {
        public Action<iObject> OnClicked;

        public iButton()
        {
            size = new Vector2(64,24);
        }

        public override void Render()
        {
            if (style == null)
            {
                style = new GUIStyle(EditorStyles.miniButton);
            }

            if(!active) return;

            GUI.enabled = enabled;
            if(GUI.Button(rect, new GUIContent(text, texture, tooltips), style))
            {
                if(OnClicked != null)
                {
                    OnClicked(this);
                }
            }
            GUI.enabled = true;
        }
    }
}