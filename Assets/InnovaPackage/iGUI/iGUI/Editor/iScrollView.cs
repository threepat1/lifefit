using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InnovaFramework.iGUI
{
    public enum iScrollViewDirection
    {
        HORIZONTAL,
        VERTICAL
    };

    public enum iScrollViewAutoSize
    {
        NONE,
        HORIZONTAL,
        VERTICAL
    }

    public class iScrollView : iObject
    {
        public iPadding padding = new iPadding();
        public iScrollViewDirection direction = iScrollViewDirection.VERTICAL;
        public iScrollViewAutoSize autoSizeMode = iScrollViewAutoSize.NONE;

        private List<iObject> children = new List<iObject>();
        private Rect containSize = new Rect();
        private Vector2 scrollPosition = new Vector2();
        

        public iScrollView()
        {

        }

        public void AddChild(iObject mObject)
        {
            if(children.Contains(mObject)) return;
            mObject.parent = this;
            children.Add(mObject);
        }

        public void RemoveChild(iObject mObject)
        {
            children.Remove(mObject);
        }

        public void RemoveChildAll()
        {
            children.Clear();
        }

        public override void Render()
        {
            if(children.Count == 0) return;
            if(!active) return;

            float count = children.Count;
            float currentPosition = position.y + padding.top;

            float maxHorizontal = 0;
            float maxVertical = 0;

            // Sorting Object
            for(int i = 0; i < count; ++i)
            {
                iObject obj = children[i];
                if(!obj.active) continue;
                currentPosition += ( i == 0 ? 0 : padding.space);

                switch(autoSizeMode)
                {
                    case iScrollViewAutoSize.HORIZONTAL:
                    {
                        obj.size.x = size.x - padding.left - padding.right;
                        break;
                    }
                    case iScrollViewAutoSize.VERTICAL:
                    {
                        obj.size.y = size.y - padding.top - padding.bottom;
                        break;
                    }
                }

                switch(direction)
                {
                    case iScrollViewDirection.VERTICAL:
                    {
                        obj.position.x = position.x + padding.left;
                        obj.position.y = currentPosition;
                        currentPosition += obj.size.y;

                        break;
                    }
                    case iScrollViewDirection.HORIZONTAL:
                    {
                        obj.position.x = currentPosition;
                        obj.position.y = position.y + padding.top;
                        currentPosition += obj.size.x;
                        break;
                    }
                }

                if(obj.right > maxHorizontal)
                {
                    maxHorizontal = obj.right;
                }

                if(obj.bottom > maxVertical)
                {
                    maxVertical = obj.bottom;
                }
            }

            containSize = new Rect();
            containSize.x = position.x;
            containSize.y = position.y;
            containSize.width = maxHorizontal;
            containSize.height = maxVertical;

            GUI.enabled = enabled;
            scrollPosition = GUI.BeginScrollView(rect, scrollPosition, containSize);
            for(int i = 0; i< children.Count; ++i)
            {
                children[i].Render();
            }
            GUI.EndScrollView();
            GUI.enabled = true;
        }


    }
}