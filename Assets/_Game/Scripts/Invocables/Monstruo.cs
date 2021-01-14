using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(Vida))]
public class Monstruo : MonoBehaviour
{
    public Estados      estado;
    public Stats        estadisticas    = new Stats();
    public Raza         raza;
    public Clase        clase;
    public int          bando           = 1;
    public Transform    objetivo;
    public List<Torre>  listaTorres;
    public Vida         vidaEnemigo;
    public List<Monstruo> listaEnemigos;
    public Busqueda[]   prioridadObjetivo;

    [Header("Eventos de cambios de estado")]
    public UnityEvent evtIdle;
    public UnityEvent evtAtacando;
    public UnityEvent evtSiguiendo;
    public UnityEvent evtParalizado;
    public UnityEvent evtMuerto;

    Vida miVida;
    private void Awake()
    {
        miVida = GetComponent<Vida>();
        if (miVida != null)
        {
            miVida.sangreInicial = estadisticas.salud;
        }
        FakeAwake();
    }
    virtual public void FakeAwake()
    {

    }
    void Start()
    {
        ControlCombate.singleton.AgregarMonstruo(this, bando);
        StartCoroutine(Maquina());
        FakeStart();
    }

    private void OnDestroy()
    {
        try
        {
            ControlCombate.singleton.QuitarMonstruo(this, bando);
        }
        catch (System.Exception)
        {
            print("ya fue removido");
            throw;
        }
    }

    virtual public void FakeStart()
    {

    }
    public IEnumerator Maquina()
    {
        switch (estado)
        {
            case Estados.idle:
                evtIdle.Invoke();
                EstadoIdle();
                break;
            case Estados.siguiendo:
                evtSiguiendo.Invoke();
                EstadoSiguiendo();
                break;
            case Estados.atacando:
                evtAtacando.Invoke();
                EstadoAtacando();
                break;
            case Estados.muerto:
                QuitarDeLaLista();
                evtMuerto.Invoke();
                EstadoMuerto();
                break;
            case Estados.paralizado:
                evtParalizado.Invoke();
                EstadoParalizado();
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.5f);  //Poner este tiempo en el control general
        StartCoroutine(Maquina());
    }

    virtual public void EstadoIdle()
    {

    }
    virtual public void EstadoSiguiendo()
    {

    }
    virtual public void EstadoAtacando()
    {

    }
    virtual public void EstadoMuerto()
    {

    }
    virtual public void EstadoParalizado()
    {

    }

    public void CambiarEstado(Estados e)
    {
        Estados estAnt = estado;
        estado = e;
        PosCambioEstado(estAnt, e);
    }

    virtual public void PosCambioEstado(Estados anterior, Estados actual)
    {

    }

    public void BuscarObjetivio()
    {
        for (int i = 0; i < prioridadObjetivo.Length; i++)
        {
            BuscarSiguiente(prioridadObjetivo[i]);
            if (objetivo != null)
            {
                break;
            }
        }
    }

    public void AsignarBando(int nBando)
    {
        bando = nBando;
    }

    public void QuitarDeLaLista()
    {
        try
        {
            ControlCombate.singleton.QuitarMonstruo(this, bando);
        }
        catch (System.Exception)
        {
            print("ya fue removido");
            throw;
        }
    }

    public void BuscarSiguiente(Busqueda b)
    {
        switch (b)
        {
            case Busqueda.Monstruos:
                BuscarEnemigo();
                break;
            case Busqueda.Torres:
                BuscarTorreEnemiga();
                break;
            case Busqueda.PuestosControl:
                break;
            default:
                break;
        }
    }

    void BuscarEnemigo()
    {
        listaEnemigos = ControlCombate.singleton.GetMonstruosEnemigos(bando);
        float d = 500000f;
        for (int i = 0; i < listaEnemigos.Count; i++)
        {
            if (listaEnemigos[i].transform != null)
            {

                float d2 = (listaEnemigos[i].transform.position - transform.position).sqrMagnitude;
                if (d2 < Mathf.Pow(estadisticas.rangoVision, 2) && d2 < d)
                {
                    objetivo = listaEnemigos[i].transform;
                    d = d2;
                }

            }

        }
    }

    void BuscarTorreEnemiga()
    {
        float d = 500000f;
        listaTorres = ControlCombate.singleton.GetTorresEnemigos(bando);
        for (int i = 0; i < listaTorres.Count; i++)
        {
            if (listaTorres[i].transform != null)
            {

                float d2 = (listaTorres[i].transform.position - transform.position).sqrMagnitude;
                if (d2 < d)
                {
                    objetivo = listaTorres[i].transform;
                    d = d2;
                }
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.up, estadisticas.rangoAtaque);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.up, estadisticas.rangoVision);
    }
#endif
}
