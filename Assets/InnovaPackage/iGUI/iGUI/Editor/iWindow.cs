using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace InnovaFramework.iGUI
{
    public class iWindow : EditorWindow 
    {
        private List<iObject> objects = new List<iObject>();

        public void AddChild(iObject obj)
        {
            objects.Add(obj);
        }
        public void RemoveChild(iObject obj) 
        {
            objects.Remove(obj);
        }

        public void Render()
        {
            foreach(iObject obj in objects)
            {
                obj.Render();
            }

            if(GUI.changed)
            {
                Repaint();
            }
        }
    }
}
