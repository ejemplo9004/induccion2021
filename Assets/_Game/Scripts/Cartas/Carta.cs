using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    public bool drageable;
    public SCCarta scCarta;
    [Header("Para generar comando")]
    public int bando;
    public int bandoCasilla;
    public string nombreCasilla;
    public CampoUso uso;
    public int invocable;

    public string comando;
    public string[] parametros;

    [Header("Interfaz")]
    public Image    imFoto;
    public Image    imEngrane;
    public Image    imFondo;
    public Image    imBoton1;
    public Image    imBoton2;
    public Image    imIcono;
    public Image    imAdorno;
    public Image    imEsfera;
    public Text     txtEstrellas;

    public void Inicializar(SCCarta _scCarta, int _bando)
    {
        scCarta     = _scCarta;
        bando       = _bando;
        comando     = scCarta.comando;
        uso         = _scCarta.campoUso;
        parametros  = scCarta.parametros;
        invocable   = _scCarta.invocable;

        imFoto.sprite       = scCarta.foto;
        imEngrane.sprite    = scCarta.perfil.engrane;
        imFondo.sprite      = scCarta.perfil.anverso;
        imBoton1.sprite     = scCarta.perfil.boton;
        imBoton2.sprite     = scCarta.perfil.boton;
        imIcono.sprite      = scCarta.perfil.icono;
        imAdorno.sprite     = scCarta.perfil.adorno;
        imEsfera.sprite     = scCarta.perfil.esfera;
        txtEstrellas.text   = scCarta.estrellas.ToString();
    }

    public void MostrarInfo()
    {
        if (ControlGeneral.singleton != null && ControlGeneral.singleton.debuguearTodo)
            Debug.Log("<color=green>" + scCarta.nombre + ": </color>" + scCarta.descripcion);
    }

    public void ActivarComando(Casilla c)
    {
        ControlCombate.singleton.gestoresRecursos[bando - 1].RestarEstrellas(scCarta.estrellas);

        string _comando = comando;
        bandoCasilla = c.bando;
        nombreCasilla = c.nombreCasilla.GetNombre();

        if(ControlGeneral.singleton.debuguearTodo || ControlCombate.singleton.debuguearCartas)
            print(_comando);

        for (int i = 0; i < parametros.Length; i++)
        {
            _comando = _comando + "|" + parametros[i];
        }

        if (ControlGeneral.singleton.debuguearTodo || ControlCombate.singleton.debuguearCartas)
            print(_comando);

        _comando = _comando.Replace("%invocable%", invocable.ToString());
        _comando = _comando.Replace("%bando%", bando.ToString());
        _comando = _comando.Replace("%bandoCasilla%", bandoCasilla.ToString());
        _comando = _comando.Replace("%nombreCasilla%", nombreCasilla.ToString());

        if (ControlGeneral.singleton.debuguearTodo || ControlCombate.singleton.debuguearCartas)
            print(_comando);

        ControlCombate.singleton.IngresarComando(_comando);
    }

    public bool EsDrageable()
    {
        drageable = ControlCombate.singleton.gestoresRecursos[bando - 1].VerificarEstrellas(scCarta.estrellas);
        return drageable;
    }
}
