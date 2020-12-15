using System.Collections;
using System.Collections.Generic;
using System.IO;
using InnovaFramework.iGUI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class iSceneNavigation : iWindow
{
    #region Public Field
    public static iSceneNavigation window;
    #endregion



    #region Private Field
    private static Rect windowRect = new Rect(0f, 0f, 255f, 255f);
    #endregion
    


    #region MonoBehavior Callback
    [MenuItem("Innova Engine/iBoost/Scene Navagation _1")]
    public static void OpenWindow()
    {
        window = GetWindow<iSceneNavigation>();
        window.titleContent = new GUIContent("Scene Navigation");
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
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            string scene = SceneUtility.GetScenePathByBuildIndex(i);

            if(!File.Exists(scene))
            {
                continue;
            }

            iSceneNavigationItem sceneItem = new iSceneNavigationItem();
            sceneItem.size = new Vector2(18, 18);
            sceneItem.text = Path.GetFileNameWithoutExtension(scene);
            sceneItem.path = scene;


            scrollView.AddChild(sceneItem);
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
