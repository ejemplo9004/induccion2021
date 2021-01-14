using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SCCarta))]
public class SCCartaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Button("vamos a ver pues");
        base.OnInspectorGUI();
    }
}
