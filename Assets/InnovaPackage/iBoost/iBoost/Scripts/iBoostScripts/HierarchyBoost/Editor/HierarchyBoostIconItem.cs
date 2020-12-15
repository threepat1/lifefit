using System.Collections;
using System.Collections.Generic;
using InnovaFramework.iGUI;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class HierarchyBoostIconItem : iObject
{
    public string path;
    private iButton button;
    private iBox icon;
    private iLabel label;

    private void Init()
    {
        button = new iButton();
        button.OnClicked = (sender) => 
        {
            foreach(var obj in Selection.gameObjects)
            {
                var icon = obj.GetComponent<HierarchyIconBoost>();
                if(path != "None")
                {
                    if(icon == null)
                    {
                        icon = obj.AddComponent<HierarchyIconBoost>();
                        icon.hideFlags = HideFlags.HideInInspector;
                    }

                    icon.content = EditorGUIUtility.IconContent(path);
                }
                else
                {
                    if(icon != null)
                    {
                        GameObject.DestroyImmediate(icon);
                    }
                }
            }

            EditorApplication.RepaintHierarchyWindow();
        };

        icon = new iBox();
        icon.size = new Vector2(16, 16);
        icon.style = new GUIStyle();

        if(path != "None")
        {
            icon.LoadBuiltInIcon( path );
        }

        label = new iLabel();
    }


    private void Update()
    {
        button.size = this.size;
        button.size.x -= 48;
        button.position = this.position + new Vector2(16 + 8, 0);

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
