using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InnovaFramework.iGUI
{
    public class iPadding
    {
        public float top;
        public float left;
        public float right;
        public float bottom;
        public float space;

        public iPadding(float top, float left, float right, float bottom, float space)
        {
            this.top = top;
            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.space = space;
        }
        public iPadding() { }
    }

    public enum iRelativePosition
    {
        RIGHT_OF,
        LEFT_OF,
        TOP_OF,
        BOTTOM_OF,
        CENTER_Y_OF,
        CENTER_X_OF,
        RIGHT_IN,
        LEFT_IN,
        TOP_IN,
        BOTTOM_IN
    }

    public enum iInputType
    {
        INT,
        FLOAT,
        STRING,
        OBJECT
    }

    public class iObject
    {
        // Properties
        public string name;
        public string tag;
        public string description;
        public string text;
        public Texture texture = null;
        public string tooltips;
        public bool active = true;
        public bool enabled = true;
        public object attechment = null; // Use for Attech some object to this object

        private bool _accessAnyWhere;
        public bool accessAnyWhere
        {
            get { return _accessAnyWhere; }
            set
            {
                _accessAnyWhere = value;
                if (_accessAnyWhere)
                {
                    iGUIUtility.SaveObjectToStore(this);
                }
                else
                {
                    iGUIUtility.DeleteObjectFromStore(this);
                }
            }
        }

        // Transform
        public Vector2 position;
        public Vector2 size;

        // Setting 
        public iObject parent;
        public GUIStyle style;
        public float labelSpace;

        // Get Only
        public float bottom { get { return position.y + size.y; } }
        public float right { get { return position.x + size.x; } }
        public Rect rect { get { return new Rect(position.x, position.y, size.x, size.y );}}




        #region Method
        public virtual void Render() { }

        public void ParseGUIContent(GUIContent content)
        {
            text = content.text;
            tooltips = content.tooltip;
            texture = content.image;
        }

        public void LoadBuiltInIcon(string name)
        {
            ParseGUIContent(EditorGUIUtility.IconContent(name));
        }

        public void RelativePosition(iRelativePosition relative, Rect rect, float space = 8)
        {
            iObject obj = new iObject();
            obj.size = rect.size;
            obj.position = rect.position;
            RelativePosition(relative, obj, space);
        }

        public void RelativePosition(iRelativePosition relative, iWindow mWindow, float space = 8)
        {
            iObject obj = new iObject();
            obj.size = mWindow.maxSize;
            RelativePosition(relative, obj, space);
        }

        public void RelativePosition(iRelativePosition relative, iObject mObject, float space = 8)
        {
            switch (relative)
            {
                case iRelativePosition.RIGHT_OF:
                    {
                        position = new Vector2()
                        {
                            x = mObject.position.x + mObject.size.x + space,
                            y = position.y
                        };

                        break;
                    }
                case iRelativePosition.LEFT_OF:
                    {
                        position = new Vector2()
                        {
                            x = mObject.position.x - size.x - space,
                            y = position.y
                        };
                        break;
                    }
                case iRelativePosition.TOP_OF:
                    {
                        position = new Vector2()
                        {
                            x = position.x,
                            y = mObject.position.y - size.y - space
                        };
                        break;
                    }
                case iRelativePosition.BOTTOM_OF:
                    {
                        position = new Vector2()
                        {
                            x = position.x,
                            y = mObject.position.y + mObject.size.y + space
                        };
                        break;
                    }
                case iRelativePosition.LEFT_IN:
                    {
                        position = new Vector2()
                        {
                            x = mObject.position.x + space,
                            y = position.y
                        };
                        break;
                    }
                case iRelativePosition.RIGHT_IN:
                    {
                        position = new Vector2()
                        {
                            x = mObject.position.x + mObject.size.x - size.x - space,
                            y = position.y
                        };
                        break;
                    }
                case iRelativePosition.TOP_IN:
                    {
                        position = new Vector2()
                        {
                            x = position.x,
                            y = mObject.position.y + space
                        };
                        break;
                    }
                case iRelativePosition.BOTTOM_IN:
                    {
                        position = new Vector2()
                        {
                            x = position.x,
                            y = mObject.position.y + mObject.size.y - size.y - space
                        };
                        break;
                    }
                case iRelativePosition.CENTER_Y_OF:
                    {
                        position = new Vector2()
                        {
                            x = position.x,
                            y = mObject.position.y + mObject.size.y / 2f - size.y / 2f
                        };
                        break;
                    }
                case iRelativePosition.CENTER_X_OF:
                    {
                        position = new Vector2()
                        {
                            x = mObject.position.x + mObject.size.x / 2f - size.x / 2f,
                            y = position.y
                        };
                        break;
                    }
            }
        }

        #endregion


    }
}