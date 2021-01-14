using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    public static Tienda singleton;
    public GameObject gmVistaPreviaTienda;
    public PreviaCarta previaCarta;
    public SCBarajaMaestra barajaMaestra;

    private void Awake()
    {
        singleton = this;
    }

    public void VistaPreviaTienda(SCCarta carta)
    {
        previaCarta.Inicializar(carta);
        SelectorVistaPrevia.singleton.Mostrar(carta.invocable);
        gmVistaPreviaTienda.SetActive(true);
    }
    public void VistaPrevia(SCCarta carta)
    {
        previaCarta.Inicializar(carta);
        SelectorVistaPrevia.singleton.Mostrar(carta.invocable);
        gmVistaPreviaTienda.SetActive(true);
    }
}
