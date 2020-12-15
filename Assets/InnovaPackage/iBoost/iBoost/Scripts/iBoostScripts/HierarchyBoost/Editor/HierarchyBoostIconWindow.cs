using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InnovaFramework.iGUI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HierarchyBoostIconWindow : iWindow
{
    #region Public Field
    public static HierarchyBoostIconWindow window;
    #endregion



    #region Private Field
    private static Rect windowRect = new Rect(0f, 0f, 256f, 512);
    private static List<string> icons = new List<string>() 
    {
        "_Help",
        "_Popup",
        "AlphabeticalSorting",
        "Animation.AddEvent",
        "Animation.FirstKey",
        "Animation.LastKey",
        "Animation.NextKey",
        "Animation.Play",
        "Animation.PrevKey",
        "Animation.Record",
        "AssemblyLock",
        "BuildSettings.Editor",
        "BuildSettings.Facebook",
        "BuildSettings.FlashPlayer",
        "BuildSettings.Metro",
        "Collab.Build",
        "Collab.BuildFailed",
        "Collab.BuildSucceeded",
        "CollabOffline",
        "console.erroricon",
        "console.warnicon",
        "DefaultSorting",
        "Favorite",
        "FilterByLabel",
        "FilterByType",
        "FilterSelectedOnly",
        "greenLight",
        "lightOff",
        "lightRim",
        "orangeLight",
        "redLight",
        "LookDevClose@2x",
        "LookDevLight@2x",
        "LookDevShadow@2x",
        "LookDevSideBySide@2x",
        "LookDevSingle1@2x",
        "LookDevSingle2@2x",
        "LookDevSplit@2x",
        "Assembly Icon",
        "AudioMixerView Icon",
        "CGProgram Icon",
        "CollabChanges Icon",
        "CollabChangesConflict Icon",
        "CollabChangesDeleted Icon",
        "CollabConflict Icon",
        "CollabCreate Icon",
        "CollabDeleted Icon",
        "CollabEdit Icon",
        "CollabExclude Icon",
        "CollabMoved Icon",
        "cs Script Icon",
        "Favorite Icon",
        "Folder Icon",
        "FolderEmpty Icon",
        "FolderFavorite Icon",
        "Avatar Icon",
        "AvatarMask Icon",
        "GameManager Icon",
        "sv_icon_dot0_pix16_gizmo",
        "sv_icon_dot1_pix16_gizmo",
        "sv_icon_dot2_pix16_gizmo",
        "sv_icon_dot3_pix16_gizmo",
        "sv_icon_dot4_pix16_gizmo",
        "sv_icon_dot5_pix16_gizmo",
        "sv_icon_dot6_pix16_gizmo",
        "sv_icon_dot7_pix16_gizmo",
        "sv_icon_dot8_pix16_gizmo",
        "sv_icon_dot9_pix16_gizmo",
        "sv_icon_dot10_pix16_gizmo",
        "sv_icon_dot11_pix16_gizmo",
        "sv_icon_dot12_pix16_gizmo",
        "sv_icon_dot13_pix16_gizmo",
        "sv_icon_dot14_pix16_gizmo",
        "sv_icon_dot15_pix16_gizmo",
        "AudioMixerController Icon",
        "DefaultAsset Icon",
        "SceneAsset Icon",
        "NavMeshData Icon",
        "NavMeshAgent Icon",
        "Animation Icon",
        "AudioSource Icon",
        "Camera Icon",
        "HingeJoint2D Icon",
        "ParticleSystemForceField Icon",
        "ReflectionProbe Icon",
        "Shader Icon",
        "Sprite Icon",
        "Light Icon",
        "WindZone Icon",
        "Profiler.Audio",
        "Profiler.CPU",
        "UnityLogo",
        "vcs_refresh",
        "winbtn_graph",
        "winbtn_mac_close",
        "winbtn_mac_max",
        "winbtn_mac_min",
        "SoftlockProjectBrowser Icon"
    };
    #endregion
    


    #region MonoBehavior Callback
    [MenuItem("GameObject/Hierarchy Boost/Set Icon", false, 0)]
    public static void OpenWindow()
    {
        if(Selection.gameObjects.Length == 0) return;
        window = GetWindow<HierarchyBoostIconWindow>();
        window.titleContent = new GUIContent("Set Icon : " + Selection.gameObjects[0].name);
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
        icons.Sort();
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
        for(int i = -1, j = icons.Count; i < j; ++i)
        {
            string path = i == -1 ? "None" : icons[i];
            HierarchyBoostIconItem iconItem = new HierarchyBoostIconItem();
            iconItem.size = new Vector2(18, 18);
            iconItem.text = path;
            iconItem.path = path;

            scrollView.AddChild(iconItem);
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