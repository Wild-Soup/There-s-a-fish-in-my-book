using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(flashbang_script))]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        flashbang_script myscript = (flashbang_script)target;

        if (GUILayout.Button("trigger"))
        {
            myscript.OnFlashBang();
        }
    }
}
