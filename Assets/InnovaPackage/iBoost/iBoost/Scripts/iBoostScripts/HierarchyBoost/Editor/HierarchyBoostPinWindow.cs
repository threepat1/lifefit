using System.Collections;
using System.Collections.Generic;
using System.IO;
using InnovaFramework.iGUI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HierarchyBoostPinWindow : iWindow
{
    #region Public Field
    public static HierarchyBoostPinWindow window;
    #endregion



    #region Private Field
    private static Rect windowRect = new Rect(0f, 0f, 255f, 255f);
    #endregion
    


    #region MonoBehavior Callback
    [MenuItem("Innova Engine/iBoost/Hierarchy Pinned _3")]
    public static void OpenWindow()
    {
        window = GetWindow<HierarchyBoostPinWindow>();
        window.titleContent = new GUIContent("Hierarchy Pinned");
        window.maxSize = windowRect.size;
        window.minSize = window.maxSize;
    }

    private void OnEnable()
    {
        if(EditorApplication.isCompiling) return;
        InitializeUI();
    }

 
    private void OnGUI()
    {
        if(EditorApplication.isCompiling)
        {
            if(window != null)
            {
                window.Close();
                return;
            }
        }

        if(window != null)
        {
            window.minSize = window.minSize;
        }

        base.Render();
    }
    #endregion

    

    #region Public Method
    #endregion



    #region Private Method
    private void InitializeUI()
    {
        iBox background = new iBox();
        background.size = windowRect.size - new Vector2(16, 16);
        background.RelativePosition(iRelativePosition.CENTER_Y_OF, windowRect);
        background.RelativePosition(iRelativePosition.CENTER_X_OF, windowRect);

        iScrollView scrollView = new iScrollView();
        scrollView.size = background.size - new Vector2(16, 16);
        scrollView.padding = new iPadding(0, 0, 0, 0, 4f);
        scrollView.autoSizeMode = iScrollViewAutoSize.HORIZONTAL;
        scrollView.RelativePosition(iRelativePosition.CENTER_X_OF, background);
        scrollView.RelativePosition(iRelativePosition.CENTER_Y_OF, background);

        DrawSceneItem(scrollView);

        RegisterGUI(background, scrollView);
    }

    private void DrawSceneItem(iScrollView scrollView)
    {
        Object[] obj = GameObject.FindObjectsOfType<HierarchyItemPin>();
        for(int i = obj.Length - 1 ; i >= 0; --i)
        {
            var o = obj[i];

            HierarchyBoostPinItem pinItem = new HierarchyBoostPinItem();
            pinItem.size = new Vector2(18, 18);
            pinItem.referenceObject = o;
            pinItem.text = o.name;
            pinItem.view = scrollView;

            //iButton btnScene = new iButton();
            //btnScene.size = new Vector2(24, 24);
            //btnScene.text = o.name;
            //btnScene.OnClicked = (sender) => 
            //{
            //    Selection.activeObject = o;
            //    EditorGUIUtility.PingObject(Selection.activeObject);
            //    if(window != null)
            //    {
            //        window.Close();
            //    }
            //};

            scrollView.AddChild(pinItem);
        }
    }

    private void RegisterGUI(params iObject[] objects)
    {
        for(int i = 0, j = objects.Length; i < j; ++i)
        {
            this.AddChild(objects[i]);
        }
    }
    #endregion
}
