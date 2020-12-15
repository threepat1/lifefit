using System.Collections;
using System.Collections.Generic;
using InnovaFramework.iGUI;
using UnityEditor;
using UnityEngine;

public class SimpleWindow : iWindow
{
    public static SimpleWindow window;
    [MenuItem("MadTool/SceneManagement %m")]
    public static void OpenWindow()
    {
        window = GetWindow<SimpleWindow>();
        window.titleContent = new GUIContent("Simple Window");
        window.maxSize = new Vector2(800, 800);
        window.minSize = window.maxSize;
    }

    private void OnEnable()
    {
        if(EditorApplication.isCompiling) return;

        var scrollView = new iScrollView();
        scrollView.autoSizeMode = iScrollViewAutoSize.HORIZONTAL;
        scrollView.direction = iScrollViewDirection.VERTICAL;
        scrollView.size = new Vector2(200, 200);
        scrollView.padding = new iPadding(4, 4, 24, 4, 4);
        for(int i = 0; i < 20; ++i)
        {
            var btn = new iButton();
            btn.text = "Index : " + i;
            btn.size = new Vector2(25, 25);

            scrollView.AddChild(btn);
        }

        var checkBox = new iCheckBox();
        checkBox.text = "Hello World";
        checkBox.RelativePosition(iRelativePosition.BOTTOM_OF, scrollView);
        checkBox.size = new Vector2(100, EditorGUIUtility.singleLineHeight);
        checkBox.OnChanged = (sender) => 
        {
            iCheckBox cb = (iCheckBox) sender;
            Debug.Log("CheckBox: " + cb.isChecked);
        };

        var dd = new iDropDown();
        dd.AddOption("Value 1", 1);
        dd.AddOption("Value 2", 2);
        dd.AddOption("Value 3", 3);
        dd.AddOption("Value 4", 3);
        dd.RelativePosition(iRelativePosition.BOTTOM_OF, checkBox);
        dd.OnChanged = (sender) =>
        {
            iDropDown d = (iDropDown)sender;
            Debug.Log("Dropdown: " + d.selectedItem + ":" + d.selectedObject);
        };

        AddChild(scrollView);
        AddChild(checkBox);
        AddChild(dd);
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
            window.minSize = window.maxSize;
        }

        base.Render();
    }
}
