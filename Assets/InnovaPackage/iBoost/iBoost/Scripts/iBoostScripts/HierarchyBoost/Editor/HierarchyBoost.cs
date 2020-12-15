using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class HierarchyBoost : MonoBehaviour
{
    #region Private Field
    private static Dictionary<HierarchyCategoryBoost, GUIContent> icons = new Dictionary<HierarchyCategoryBoost, GUIContent>();
    private static GUIContent pinEnabled = new GUIContent();
    private static GUIContent pinDisabled = new GUIContent();

    private static Color currentColor = new Color(0.76f, 0.76f, 0.76f);
    private static float currentAlpha = 0.1f;
    #endregion
    

    #region MonoBehavior Callback
    static HierarchyBoost()
    {
        icons[HierarchyCategoryBoost.STANDARD]  = EditorGUIUtility.IconContent("Favorite Icon");
        icons[HierarchyCategoryBoost.SCRIPT]    = EditorGUIUtility.IconContent("cs Script Icon");
        icons[HierarchyCategoryBoost.CANVAS]    = EditorGUIUtility.IconContent("RectTransform Icon");
        icons[HierarchyCategoryBoost.OBJECT_3D] = EditorGUIUtility.IconContent("BoxCollider Icon");
        icons[HierarchyCategoryBoost.OBJECT_2D] = EditorGUIUtility.IconContent("BoxCollider2D Icon");
        icons[HierarchyCategoryBoost.CAMERA]    = EditorGUIUtility.IconContent("Camera Icon");
        icons[HierarchyCategoryBoost.LIGHTING]  = EditorGUIUtility.IconContent("Light Icon");

        pinDisabled = EditorGUIUtility.IconContent("TestNormal");
        pinEnabled  = EditorGUIUtility.IconContent("sv_icon_dot3_pix16_gizmo");

        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    [MenuItem( "GameObject/Hierarchy Boost/Create Header", false, 0 ) ]
    public static void CreateSperator()
    {
        GameObject header = new GameObject();
        var script = header.AddComponent<HierarchyItemBoost>();
        script.title = "Header"; 
        script.name = "";

        if(Selection.activeTransform != null)
        {
            header.transform.SetParent(Selection.activeTransform);
            Selection.activeGameObject = header;
        }
    }
    #endregion



    #region Private Method
    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if(obj != null)
        {
            HierarchyItemBoost itemBoost = obj.GetComponent<HierarchyItemBoost>();
            if(itemBoost != null)
            {
                Transform t = itemBoost.transform;
                if(t.position != Vector3.zero)
                {
                    t.position = Vector3.zero;
                }
                if(itemBoost.name != string.Empty)
                {
                    itemBoost.name = string.Empty;
                }
                if(t.localScale != Vector3.one)
                {
                    t.localScale = Vector3.one;
                }
                if(t.rotation != Quaternion.identity)
                {
                    t.rotation = Quaternion.identity;
                }

                DrawBoostItem(itemBoost, instanceID, ref selectionRect);
            }
            else
            {
                DrawNoneBoostItem(ref obj, instanceID, ref selectionRect);
                DrawPin(ref obj, instanceID, ref selectionRect);
            }

            DrawBothItem(ref obj, instanceID, ref selectionRect);
        }
    }


    private static void DrawBothItem(ref GameObject obj, int instanceID, ref Rect selectionRect)
    {
        Transform t = obj.transform;

        float multiply = 1f;

        while(t != null)
        {
            if(!(multiply == 1f && t.childCount > 0))
            {
                Rect iconTemp = new Rect(selectionRect.position + new Vector2((-14f * multiply) + 6f, 0), new Vector2(1f, selectionRect.size.y));
                //Color c = new Color(0.65f, 0.65f, 0.65f);
                Color c = new Color(0, 0, 0, 0.075f);
                EditorGUI.DrawRect(iconTemp, c);
            }

            t = t.parent;   
            multiply++;
        }

        var icon = obj.GetComponent<HierarchyIconBoost>();
        if(icon != null)
        {
            //Rect iconRect = new Rect(selectionRect.position, new Vector2(selectionRect.size.y, selectionRect.size.y));
            Rect iconRect = new Rect(new Vector2(selectionRect.position.x + selectionRect.size.x - selectionRect.size.y * 2 - 8, selectionRect.position.y) , 
                                     new Vector2(selectionRect.size.y, selectionRect.size.y));
            //EditorGUI.DrawRect(iconRect, new Color(0.76f, 0.76f, 0.76f));
            GUI.Box(iconRect, icon.content, new GUIStyle());
        }
    }


    private static void DrawBoostItem(HierarchyItemBoost itemBoost, int instanceID, ref Rect selectionRect)
    {
        Rect iconRect = new Rect(selectionRect.position + new Vector2(-2f, 0), new Vector2(selectionRect.size.y, selectionRect.size.y));
        Rect bgRect = new Rect(selectionRect.position + new Vector2(selectionRect.size.y, 0), selectionRect.size + new Vector2(0, 1f));
        Rect bgRect2 = new Rect(selectionRect.position, selectionRect.size);
        Rect textRect = new Rect(bgRect.position + new Vector2(0, 1f), selectionRect.size);


        // Background
        EditorGUI.DrawRect(bgRect2, new Color(0.76f, 0.76f, 0.76f));
        GUIStyle style = null;

        switch(itemBoost.backgroundBoost)
        {
            case HierarchyBackgroundBoost.HELPBOX:
            {
                style = new GUIStyle(EditorStyles.helpBox);
                break;
            }
            case HierarchyBackgroundBoost.SOLID:
            {
                style = new GUIStyle();
                style.normal.background = EditorGUIUtility.whiteTexture;
                break;
            }
            case HierarchyBackgroundBoost.NONE:
            {
                style = new GUIStyle();
                break;
            }
            case HierarchyBackgroundBoost.TOOLBAR:
            {
                style = new GUIStyle(EditorStyles.toolbar);
                break;
            }
            case HierarchyBackgroundBoost.BUTTON:
            {
                style = new GUIStyle(EditorStyles.miniButton);
                break;
            }
        }

        var c = GUI.backgroundColor;
        GUI.backgroundColor = itemBoost.backgroundColor;
        GUI.Box(bgRect, "", style);
        GUI.backgroundColor = c;


        // Icon
        if (itemBoost.categoryBoost != HierarchyCategoryBoost.NONE)
        {
            GUIContent content = icons[HierarchyCategoryBoost.STANDARD];
            if (icons.ContainsKey(itemBoost.categoryBoost))
            {
                content = icons[itemBoost.categoryBoost];
            }
            GUI.Box(iconRect, content, new GUIStyle());
        }


        // Text Title
        GUIStyle styleText = new GUIStyle();
        styleText.normal = new GUIStyleState() { textColor = itemBoost.titleColor };
        styleText.fontStyle = FontStyle.Bold;
        EditorGUI.LabelField(textRect, " " + itemBoost.title + "", styleText);

        currentColor = itemBoost.backgroundColor;
        currentAlpha = itemBoost.backgroundChildAlpha;

        if(itemBoost.backgroundBoost == HierarchyBackgroundBoost.NONE)
        {
            GameObject obj = itemBoost.gameObject;
            DrawNoneBoostItem(ref obj, instanceID, ref selectionRect);
        }
    }


    private static void DrawNoneBoostItem(ref GameObject obj, int instanceID, ref Rect selectionRect)
    {
        Color c = currentColor;
        c.a = currentAlpha;

        float offset = 16;

        Rect rect = new Rect(selectionRect.position, selectionRect.size + new Vector2(offset + 16f, 0));
        EditorGUI.DrawRect(rect, c);
    }


    private static void DrawPin(ref GameObject obj, int instanceID, ref Rect selectionRect)
    {
        GUIContent content = null;
        HierarchyItemPin pin = obj.GetComponent<HierarchyItemPin>();

        Rect rect = new Rect(new Vector2(selectionRect.position.x + selectionRect.size.x - selectionRect.size.y, selectionRect.position.y) , 
                             new Vector2(selectionRect.size.y, selectionRect.size.y));
        if(pin == null)
        {
            content = pinDisabled;
            rect.position -= new Vector2(2, 1);
        }
        else
        {
            content = pinEnabled;
        }


        if(GUI.Button(rect, content, new GUIStyle()))
        {
            if(pin == null)
            {
                var pinned = obj.AddComponent<HierarchyItemPin>();
                pinned.hideFlags = HideFlags.HideInInspector;

            }
            else
            {
                DestroyImmediate(pin);
            }
        }
    }
    #endregion
}
