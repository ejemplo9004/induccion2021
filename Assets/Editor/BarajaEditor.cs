using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Baraja))]
public class BarajaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Barajar"))
        {
            Baraja b = (Baraja)target;
            b.Barajar();
        }
        if (GUILayout.Button("A String"))
        {
            Baraja b = (Baraja)target;
            b.ConvertirATexto();
        }
        if (GUILayout.Button("Desde String"))
        {
            Baraja b = (Baraja)target;
            b.ConvertirDesdeTexto();
        }
        if (GUILayout.Button("Descomprimir"))
        {
            Baraja b = (Baraja)target;
            b.Descomprimir();
        }
        if (GUILayout.Button("Comprimir"))
        {
            Baraja b = (Baraja)target;
            b.Comprimir();
        }
    }
}
