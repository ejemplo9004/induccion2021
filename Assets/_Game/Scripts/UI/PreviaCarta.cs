using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviaCarta : MonoBehaviour
{
    public Text txtEstrellas;
    public Text txtFuerza;
    public Text txtRango;
    public Text txtVelociad;
    public Text txtRaza;
    public Text txtClase;
    public Text txtNombre;
    public Text txtDescripcion;
    public Text txtDescripcionDependencia;

    public Image imFondo;
    public Image imEngrane;
    public Image imEsfera;
    public Image imLogo;
    public Image imBarra1;
    public Image imBarra2;

    public SCBarajaMaestra barajaMaestra;

    public void Inicializar(SCCarta carta)
    {
        Stats estadisticas = barajaMaestra.invocables[carta.invocable].GetComponent<Monstruo>().estadisticas;
        txtEstrellas.text = carta.estrellas.ToString("00");
        txtFuerza.text = estadisticas.fuerza.ToString("0.0");
        txtRango.text = estadisticas.rangoAtaque.ToString("0.0");
        txtVelociad.text = estadisticas.velocidad.ToString("0.0");
        txtRaza.text = carta.raza.ToString();
        txtClase.text = barajaMaestra.invocables[carta.invocable].GetComponent<Monstruo>().clase.ToString();
        txtNombre.text = carta.nombre;
        txtDescripcion.text = carta.descripcion;

        imFondo.sprite = carta.perfil.anverso;
        imEngrane.sprite = carta.perfil.engrane;
        imEsfera.sprite = carta.perfil.esfera;
        imLogo.sprite = carta.perfil.icono;
        imBarra1.sprite = carta.perfil.boton;
        imBarra2.sprite = carta.perfil.boton;
    }

}
