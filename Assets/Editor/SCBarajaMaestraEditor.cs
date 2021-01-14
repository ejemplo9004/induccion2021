using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        SCBarajaMaestra obj = EditorUtility.InstanceIDToObject(instanceId) as SCBarajaMaestra;
        if (obj != null)
        {
            SCBarajaMaestraEditorWindow.Abrir(obj);
            return true;
        }
        return false;
    }
}

[CustomEditor(typeof(SCBarajaMaestra))]
public class SCBarajaMaestraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Abrir ventana del editor"))
        {
            SCBarajaMaestraEditorWindow.Abrir((SCBarajaMaestra)target);
        }
    }
}
