using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUICartas : MonoBehaviour
{
    public List<Casilla>    casillas;
    public RectTransform[]  rtBases;
    public SCBarajaMaestra  barajaMaestra;
    public Transform        mano;
    public GameObject       prCarta;

    public delegate void DelegadoJaladas();

    public DelegadoJaladas delegadoJaladas;

    public static ControlUICartas singleton;

    private void Awake()
    {
        singleton = this;
    }
    private void Start()
    {
        OcultarTodo();
    }

    public void AgregarCasilla(Casilla c)
    {
        casillas.Add(c);
        Ocultar(c.gameObject);
    }

    public void MostrarTodo(CampoUso uso)
    {
        LeanTween.cancelAll();
        for (int i = 0; i < casillas.Count; i++)
        {
            //print(Verificar(casillas[i], (int)uso) + " para: " + uso + "(" + (int) uso + ")");
            if(Verificar(casillas[i], (int)uso)) Mostrar(casillas[i].gameObject);
        }
        for (int i = 0; i < rtBases.Length; i++)
        {
            LeanTween.alpha(rtBases[i], 1, 0.3f);
        }

        bool Verificar(Casilla c, int uss)
        {
            return (c.bando == (int)uso || uss == 0);
        }
    }
    public void OcultarTodo()
    {
        LeanTween.cancelAll();
        for (int i = 0; i < casillas.Count; i++)
        {
            Ocultar(casillas[i].gameObject);
        }
        for (int i = 0; i < rtBases.Length; i++)
        {
            LeanTween.alpha(rtBases[i], 0, 0.3f);
        }
    }

    void Ocultar(GameObject g)
    {
        LeanTween.alpha(g.GetComponent<RectTransform>(), 0, 0.5f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scaleY(g, 0, 0.5f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scaleX(g, 0, 0.5f).setEase(LeanTweenType.easeOutExpo);
    }
    void Mostrar(GameObject g)
    {
        LeanTween.alpha(g.GetComponent<RectTransform>(), 1, 0.1f).setEase(LeanTweenType.easeInExpo);
        LeanTween.scaleY(g, 1, 0.1f).setEase(LeanTweenType.easeInExpo);
        LeanTween.scaleX(g, 1, 0.1f).setEase(LeanTweenType.easeInExpo);
    }

    public void JalarCarta(ElementoBaraja cual, int bando)
    {
        SCCarta carta =  barajaMaestra.GetCarta(cual);
        GameObject cartaGO = Instantiate(prCarta, mano) as GameObject;
        Carta cartaCa = cartaGO.GetComponent<Carta>();
        cartaCa.Inicializar(carta, bando);
    }
}
