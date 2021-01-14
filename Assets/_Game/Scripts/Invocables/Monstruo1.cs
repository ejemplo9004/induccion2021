using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monstruo1 : Monstruo
{
    NavMeshAgent            miAgente;
    public Animator         miAnimator;
    public float            tiempoMaximoSeguir = 15;

    private float tiempoAtacar=0;
    float tiempoEmpezoSeguir;
    public override void FakeAwake()
    {
        miAgente = GetComponent<NavMeshAgent>();
        miAgente.speed = estadisticas.velocidad;
    }
    override public void EstadoAtacando()
    {
        if (objetivo == null)
        {
            CambiarEstado(Estados.idle);
            return;
        }
        if(miAgente != null) miAgente.SetDestination(transform.position);
        transform.LookAt(objetivo); 

        float d2 = (objetivo.position - transform.position).sqrMagnitude;
        if (d2 > Mathf.Pow(estadisticas.rangoAtaque, 2))
        {
            CambiarEstado(Estados.siguiendo);
            return;
        }
        if (Time.time > tiempoAtacar)
        {
            if (vidaEnemigo == null)
            {
                CambiarEstado(Estados.idle);
            }
            else
            {
                Atacar();
                tiempoAtacar = Time.time + estadisticas.periodoDaño;
            }
        }
    }

    public void Atacar()
    {
        if (vidaEnemigo != null && vidaEnemigo.vivo)
        {
            vidaEnemigo.CausarDaño(estadisticas.fuerza);
        }
        else
        {
            CambiarEstado(Estados.idle);
        }
    }

    public override void EstadoIdle()
    {
        BuscarObjetivio();
        if (objetivo != null)
        {
            CambiarEstado(Estados.siguiendo);
        }
    }

    public override void EstadoSiguiendo()
    {
        if (objetivo == null)
        {
            CambiarEstado(Estados.idle);
            return;
        }
        if (miAgente != null && objetivo != null) miAgente.SetDestination(objetivo.position);
        if ((objetivo.position-transform.position).sqrMagnitude < Mathf.Pow(estadisticas.rangoAtaque, 2))
        {
            vidaEnemigo = objetivo.GetComponent<Vida>();
            CambiarEstado(Estados.atacando);
        }
        if ((Time.time - tiempoEmpezoSeguir)>tiempoMaximoSeguir)
        {
            CambiarEstado(Estados.idle);
        }
        if (Random.Range(0f,1f) > 0.7)
        {
            BuscarObjetivio();
        }
    }

    public override void PosCambioEstado(Estados anterior, Estados actual)
    {
        if (anterior == Estados.siguiendo && actual != Estados.siguiendo)
        {
            miAgente.SetDestination(transform.position);
        }
        if (miAnimator != null)
        {
            miAnimator.SetInteger("estado", (int)estado);
        }
        if (actual == Estados.siguiendo)
        {
            tiempoEmpezoSeguir = Time.time;
        }
    }
}
