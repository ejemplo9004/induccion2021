using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorVistaPrevia : MonoBehaviour
{
    public static SelectorVistaPrevia singleton;
    public GameObject[] objetos;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        Mostrar(-1);
    }

    public void Mostrar(int cual)
    {
        for (int i = 0; i < objetos.Length; i++)
        {
            objetos[i].SetActive(i == cual);
        }
    }
}
