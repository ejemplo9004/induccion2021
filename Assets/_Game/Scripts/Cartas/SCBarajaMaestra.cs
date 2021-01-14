using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Baraja maestra", menuName = "Morion/Baraja Maestra")]
[System.Serializable]
public class SCBarajaMaestra : ScriptableObject
{
    public TiposBaraja tipo;
    public List<SCCarta> cartas1 = new List<SCCarta>();
    public List<SCCarta> cartas2 = new List<SCCarta>();
    public List<SCCarta> cartas3 = new List<SCCarta>();
    public List<SCCarta> cartas4 = new List<SCCarta>();
    public List<SCCarta> cartas5 = new List<SCCarta>();
    public int numeroBarajas = 5;

    [Header("Perfiles")]
    public PerfilGraficoCarta[] perfiles;
    [Header("Invocables")]
    public GameObject[] invocables;
    public void CrearNuevaCarta()
    {
        SCCarta nc = new SCCarta();
        GetLista((int) tipo).Add(nc);
    }

    public SCCarta GetCarta(ElementoBaraja cual)
    {
        return GetLista((int)cual.tipo)[cual.carta];
    }
    public SCCarta GetCarta(ElementoBaraja cual, int _carta)
    {
        return GetLista((int)cual.tipo)[_carta];
    }
    public SCCarta GetCarta(int _tipo, int cual)
    {
        return GetLista(_tipo)[cual];
    }

    public List<SCCarta> GetLista(int i)
    {
        switch (i)
        {
            case 1:
                return cartas1;
            case 2:
                return cartas2;
            case 3:
                return cartas3;
            case 4:
                return cartas4;
            case 5:
                return cartas5;
            default:
                break;
        }
        return null;
    }


    public List<SCCarta> GetListaCompleta()
    {
        List<SCCarta> lista = new List<SCCarta>();
        for (int b = 1; b < numeroBarajas; b++)
        {
            List<SCCarta> cartas0 = GetLista(b);
            for (int i = 0; i < cartas0.Count; i++)
            {
                lista.Add(cartas0[i]);
            }
        }
        return lista;
    }
}

public enum TiposBaraja
{
    apoyo = 1,
    comunicaciones = 2,
    docencia = 3,
    navegacion = 4,
    proteccion = 5
}