using System.Collections;
using System.Collections.Generic;
using InnovaFramework.iGUI;
using UnityEngine;
using UnityEditor;

public class HierarchyBoostPinItem : iObject
{
    public Object referenceObject;
    public iScrollView view;

    private iButton button;
    private iButton unPin;

    private iBox icon;
    private iLabel label;

    private void Init()
    {
        button = new iButton();
        button.OnClicked = (sender) => 
        {
            Selection.activeObject = referenceObject;
            EditorGUIUtility.PingObject(Selection.activeObject);
            if(HierarchyBoostPinWindow.window != null)
            {
                HierarchyBoostPinWindow.window.Close();
            }
        };

        unPin = new iButton();
        unPin.style = new GUIStyle();
        unPin.LoadBuiltInIcon("CollabDeleted Icon");
        unPin.OnClicked = (sender) =>
        {
            GameObject.DestroyImmediate(referenceObject);
            view.RemoveChild(this);
        };

        icon = new iBox();
        icon.size = new Vector2(18, 18);
        icon.style = new GUIStyle();

        icon.LoadBuiltInIcon( "GameObject Icon");

        label = new iLabel();
    }


    private void Update()
    {
        button.size = this.size;
        button.size.x -= this.size.y + 8;
        button.size.x -= 16 + 8;
        button.position = this.position + new Vector2(16 + 8, 0);

        unPin.size = new Vector2(this.size.y, this.size.y);
        unPin.RelativePosition(iRelativePosition.RIGHT_OF, button, 4);
        unPin.RelativePosition(iRelativePosition.CENTER_Y_OF, button);

        icon.position = this.position + new Vector2(2, 0);
        icon.RelativePosition(iRelativePosition.CENTER_Y_OF, button);

        label.SetText(this.text);
        label.RelativePosition(iRelativePosition.CENTER_Y_OF, button);
        label.RelativePosition(iRelativePosition.LEFT_IN, button);
        label.position.y -= 0.1f;
    }

    private void Draw()
    {
        button.Render();
        unPin.Render();
        icon.Render();
        label.Render();
    }


    public override void Render()
    {
        if(button == null)
        {
            Init();
        }

        Update();
        Draw(); 
    }
}
