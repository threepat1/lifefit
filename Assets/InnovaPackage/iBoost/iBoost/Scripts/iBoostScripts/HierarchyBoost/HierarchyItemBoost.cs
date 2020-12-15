using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HierarchyCategoryBoost
{
    STANDARD,
    SCRIPT,
    OBJECT_3D,
    OBJECT_2D,
    CAMERA,
    LIGHTING,
    CANVAS,
    NONE
}


public enum HierarchyBackgroundBoost
{
    NONE,
    SOLID,
    HELPBOX,
    TOOLBAR,
    BUTTON
}


[DisallowMultipleComponent]
public class HierarchyItemBoost : MonoBehaviour
{
    public string title;
    public Color titleColor = Color.black;
    public Color backgroundColor = new Color(0.76f, 0.76f, 0.76f);
    public float backgroundChildAlpha = 0.1f;

    public HierarchyCategoryBoost categoryBoost;
    public HierarchyBackgroundBoost backgroundBoost = HierarchyBackgroundBoost.HELPBOX;
}
