using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercambiable : MonoBehaviour
{
    public static float tp = 0;
    public int indice = 0;
    public GameObject[] elementos;

    private float mtp;
    void Start()
    {
        mtp = 20 * tp;
        tp++;
        CambiarElemento();
    }

    [ContextMenu("Siguiente")]
    public void Siguiente()
    {
        indice = (indice+1) % elementos.Length;
        indice = Mathf.Clamp(indice, 0, elementos.Length - 1);
        CambiarElemento();
    }

    [ContextMenu("Anterior")]
    public void Anterior()
    {
        indice --;
        indice = Mathf.Clamp(indice, 0, elementos.Length - 1);
        CambiarElemento();
    }

    void CambiarElemento()
    {
        for (int i = 0; i < elementos.Length; i++)
        {
            elementos[i].SetActive(i == indice);
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0f, mtp, 20, 20), "-"))
        {
            Anterior();
        }
        if (GUI.Button(new Rect(30f, mtp, 20, 20), "+"))
        {
            Siguiente();
        }
    }
}
