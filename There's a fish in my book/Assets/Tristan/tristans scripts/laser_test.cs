using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(laser_VFX_script))]
public class laser_test : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.UpdateIfRequiredOrScript();

        laser_VFX_script script = (laser_VFX_script)target;

        if (GUILayout.Button("Ya mom"))
            script.SwitchSaber();
        serializedObject.ApplyModifiedProperties();
    }
    
}
#endif
