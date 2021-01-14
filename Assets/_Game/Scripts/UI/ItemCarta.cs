using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCarta : MonoBehaviour
{
    public Image imFoto;
    public Text txtNombre;
    public Text txtDescripcion;
    public Text txtCantidad;

    public SCCarta carta;
    public void Inicializar(SCCarta _carta, int cantidad)
    {
        carta = _carta;
        imFoto.sprite = carta.foto;
        txtNombre.text = carta.nombre;
        txtDescripcion.text = carta.descripcion;
        
        txtCantidad.text = cantidad.ToString("00");
    }
    public void InicializarTienda(SCCarta _carta, int cantidad)
    {
        carta = _carta;
        imFoto.sprite = carta.foto;
        txtNombre.text = carta.nombre;
        txtDescripcion.text = carta.descripcion;

        txtCantidad.text = "Ք " + cantidad.ToString("000");
    }

    public void ActivarTienda()
    {
        Tienda.singleton.VistaPreviaTienda(carta);
    }
    public void ActivarNormal()
    {
        Tienda.singleton.VistaPrevia(carta);
    }

}
