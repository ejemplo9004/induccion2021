using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Baraja))]
public class Inventario : MonoBehaviour
{
    private Baraja baraja;
    public GameObject itemCarta;
    public GameObject itemTienda;
    public GameObject LineaSeparadora;
    public Transform padreItemCartas;
    public Transform padreItemTienda;
    public SCBarajaMaestra barajaMaestra;

    private void Awake()
    {
        baraja = GetComponent<Baraja>();
    }
    void Start()
    {
        baraja.CargarCartas();
        for (int i = 0; i < baraja.cartasComprimidas.Count; i++)
        {
            ItemCarta itc = (Instantiate(itemCarta, padreItemCartas) as GameObject).GetComponent<ItemCarta>();
            itc.Inicializar(barajaMaestra.GetCarta(baraja.cartasComprimidas[i]), baraja.cartasComprimidas[i].cantidad);
            itc.gameObject.SetActive(true);
        }

        for (int i = 1; i < 6; i++)
        {
            TiposBaraja t = (TiposBaraja)i;
            (Instantiate(LineaSeparadora, padreItemTienda) as GameObject).GetComponentInChildren<Text>().text = t.ToString().ToUpper();
            List<SCCarta> cartas = barajaMaestra.GetLista(i);
            for (int j = 0; j < cartas.Count; j++)
            {
                ItemCarta itc = (Instantiate(itemTienda, padreItemTienda) as GameObject).GetComponent<ItemCarta>();
                itc.InicializarTienda(barajaMaestra.GetCarta(i, j), barajaMaestra.GetCarta(i, j).precio);
                itc.gameObject.SetActive(true);
            }
        }

    }

}
