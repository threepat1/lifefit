using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;


namespace InnovaFramework.iGUI
{

    public class iDragAndDrop : iObject
    {
        public Action<iObject> OnDragUpdate;
        public Action<iObject> OnDrapPerform;

        public iDragAndDrop()
        {
            size = new Vector2(100, 100);
        }

        public override void Render()
        {
            if (style == null)
            {
                style = new GUIStyle(EditorStyles.helpBox);
            }

            if(!active) return;

            GUI.enabled = enabled;
            GUI.Box(new Rect(position, size), new GUIContent(text, texture, tooltips), style);
            ProcessDND();
            GUI.enabled = false;
        }

        private void ProcessDND()
        {
            if(GUI.enabled == false) return;

            Event evt = Event.current;
            if (new Rect(position, size).Contains(Event.current.mousePosition))
            {
                if (evt.type == EventType.DragUpdated)
                {
                    if (OnDragUpdate != null)
                    {
                        OnDragUpdate(this);
                        Event.current.Use();
                    }
                }
                else if (evt.type == EventType.DragPerform)
                {
                    if (OnDrapPerform != null)
                    {
                        OnDrapPerform(this);
                        Event.current.Use();
                    }
                }
            }
        }


    }

}