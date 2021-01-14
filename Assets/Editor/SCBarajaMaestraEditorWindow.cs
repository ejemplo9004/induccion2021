using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SCBarajaMaestraEditorWindow : ExtendedEditorWindow
{
    public static SCBarajaMaestra baraja;
    public bool recargar;
    public static void Abrir(SCBarajaMaestra barajaMaestra)
    {
        SCBarajaMaestraEditorWindow ventana = GetWindow<SCBarajaMaestraEditorWindow>("Baraja Maestra");
        ventana.serializedObject = new SerializedObject(barajaMaestra);
        baraja = barajaMaestra;
    }

    private void OnGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tipo"));
        DibujarBaraja((int)(baraja.tipo));
        Aplicar();
    }

    void DibujarBaraja(int cual)
    {
        currentProperty = serializedObject.FindProperty("cartas" + cual.ToString());
        GUILayout.Button("Cartas " + cual);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("Box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        DrawSideBar(currentProperty);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true));
        if (selectedProperty != null)
        {
            DrawProperties(selectedProperty, true);
        }
        else
        {
            EditorGUILayout.LabelField("No se ha seleccionado ninguna carta");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    void Aplicar()
    {
        serializedObject.ApplyModifiedProperties();
    }
    public override void CrearNuevaCarta()
    {
        //baraja.CrearNuevaCarta();
        SCCarta nc = new SCCarta();
        baraja.GetLista((int)baraja.tipo).Add(nc);
        Aplicar();
        Close();
        Abrir(baraja);
    }
}
