using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
public class HierarchyItemPin : MonoBehaviour
{

}


#if UNITY_EDITOR
[CustomEditor(typeof(HierarchyItemPin))]
public class HierarchyItemPinEditor : Editor
{
    private static readonly string[] _dontIncludeMe = new string[]{"m_Script"};
     
     public override void OnInspectorGUI()
     {
         serializedObject.Update();
 
         DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
 
         serializedObject.ApplyModifiedProperties();
     }
}
#endif