using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public float    sangreInicial;
    public float    sangreActual;
    public Monstruo monstruo;
    public Torre    torre;
    public bool     destruirAlMorir = true;
    public float    tiempoDestruir;
    public bool     vivo = true;
    public bool     imprimir = false;
    public Image    imSangre;

    private void Start()
    {
        sangreActual = sangreInicial;
    }
    public void CausarDaño(float cuanto)
    {
        sangreActual -= cuanto;
        if (sangreActual < 0)
        {
            sangreActual = 0;
        }
        if (sangreActual <= 0 && vivo)
        {
            Morir();
            vivo = false;
        }
        ActualizarGraficos();
    }

    void ActualizarGraficos()
    {
        if (imSangre != null)
        {
            imSangre.fillAmount = sangreActual / sangreInicial;
        }
    }

    public void Morir()
    {
        if (monstruo != null)
        {
            monstruo.CambiarEstado(Estados.muerto);
        }
        if (torre != null)
        {
            torre.Morir();
        }
        if (destruirAlMorir)
        {
            Destroy(gameObject, tiempoDestruir);
            if(imprimir) print("Destruir en " + tiempoDestruir);
        }
    }

    private void OnDestroy()
    {
        if (imprimir) print("Destruido");
    }
}
