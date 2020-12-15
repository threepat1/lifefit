using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InnovaFramework.iGUI
{
    public class iLabel : iObject 
    {
        public void Resize()
        {
            if(style == null)
            {
                style = new GUIStyle(EditorStyles.label);
            }
            size = style.CalcSize(new GUIContent(text));
        }

        public void SetText(string text, bool autoSize = true)
        {
            this.text = text;
            if(autoSize)
            {
                Resize();
            }
        }

        public override void Render()
        {
            if(style == null)
            {
                style = new GUIStyle(EditorStyles.label);
            }

            if(!active) return;

            GUI.enabled = enabled;

            GUI.Label
            ( 
                new Rect(position, size),
                new GUIContent(text, texture, tooltips),
                style
            );

            GUI.enabled = true;
        }
    }
}
