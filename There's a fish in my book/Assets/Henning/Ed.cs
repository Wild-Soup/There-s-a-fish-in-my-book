using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SlottsMachine))]
public class Ed : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SlottsMachine lol = (SlottsMachine)target;

        if (GUILayout.Button("SPINNNNNNN"))
            lol.StartCoroutine(lol.SpinMachine());
    }
}
