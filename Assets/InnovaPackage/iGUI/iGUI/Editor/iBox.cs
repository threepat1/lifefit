using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InnovaFramework.iGUI
{
    public class iBox : iObject
    {
        public iBox()
        {
            size = new Vector2(64, 64);
        }

        public override void Render()
        {
            if(style == null)
            {
                style = new GUIStyle(EditorStyles.helpBox);
            }

            if(!active) return;

            GUI.enabled = enabled;
            GUI.Box
            (
                rect,
                new GUIContent(text, texture, tooltips),
                style
            );
            GUI.enabled = true;
        }
    }

}