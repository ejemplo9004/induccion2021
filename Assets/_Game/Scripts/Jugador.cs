using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Baraja))]
public class Jugador : MonoBehaviour
{
    public Baraja baraja;
    public int bando;
    private void Awake()
    {
        if (baraja == null)
        {
            baraja = GetComponent<Baraja>();
        }
    }

    private void Start()
    {
        baraja.CargarCartas();
        baraja.Barajar();
        for (int i = 0; i < 4; i++)
        {
            Jalar();
        }
        ControlUICartas.singleton.delegadoJaladas += SubJalar;
    }
    public void SubJalar()
    {
        Invoke("Jalar", 1f);
    }
    public void Jalar()
    {
        if (baraja.cartasDescomprimidas.Count >0)
        {
            ControlUICartas.singleton.JalarCarta(baraja.cartasDescomprimidas[0], bando);
            baraja.cartasDescomprimidas.RemoveAt(0);
        }
    }

}
