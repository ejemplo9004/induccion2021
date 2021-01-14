using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Vida))]
public class Torre : MonoBehaviour
{
    public int          bando;
    public float        rangoAtaque     = 3;
    public float        periodoAtaque   = 2;
    public float        daño            = 2;
    public Vida         vidaEnemigo;
    List<Monstruo>      listaEnemigos;
    public GameObject   particulasMuerte;
    public GameObject   grafico;
    public LineRenderer laser;
    public bool         usarLaser;
    public Vector3[]    offsets;
    public bool         vivo            = true;
    public Transform    torreta;
    public Transform    cañon;
    public ParticleSystem   particulas;

    private void Start()
    {
        if (laser == null)
        {
            laser = GetComponent<LineRenderer>();
        }
        ControlCombate.singleton.AgregarTorre(this, bando);
        StartCoroutine(Atacar());
        StartCoroutine(ActualizarPosicionLaser());
    }
    private void OnDestroy()
    {
        ControlCombate.singleton.QuitarTorre(this, bando);
    }

    IEnumerator ActualizarPosicionLaser()
    {
        while (true)
        {
            if (laser.enabled || !usarLaser)
            {
                if (vidaEnemigo == null)
                {
                    DesactivarLinea();
                }
                else
                {
                    if (laser != null) laser.SetPosition(1, vidaEnemigo.transform.position + offsets[1]);

                    Vector3 direccionEnemigo = (vidaEnemigo.transform.position - transform.position).normalized;
                    Vector3 derecha = Vector3.Cross(transform.up, direccionEnemigo);
                    Vector3 frente = Vector3.Cross(transform.up, derecha);
                    torreta.forward = frente;
                    torreta.Rotate(180 * Vector3.up);
                    cañon.LookAt(vidaEnemigo.transform);
                    
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator Atacar()
    {
        while (true)
        {
            yield return new WaitForSeconds(periodoAtaque);
            BuscarEnemigo();
            if (vivo && vidaEnemigo != null)
            {
                vidaEnemigo.CausarDaño(daño);
                if (particulas!=null)
                {
                    particulas.Play();
                } 
            }
        }
    }

    void BuscarEnemigo()
    {
        if (!vivo)
        {
            return;
        }
        listaEnemigos = ControlCombate.singleton.GetMonstruosEnemigos(bando);
        float d = 500000f;
        for (int i = 0; i < listaEnemigos.Count; i++)
        {
            if (listaEnemigos[i].transform != null)
            {

                float d2 = (listaEnemigos[i].transform.position - transform.position).sqrMagnitude;
                if (d2 < Mathf.Pow(rangoAtaque, 2) && d2 < d)
                {
                    vidaEnemigo = listaEnemigos[i].transform.GetComponent<Vida>();
                    ActivarLinea();
                    d = d2;
                }
            }

        }
        if (vidaEnemigo == null && laser != null && laser.enabled)
        {
            DesactivarLinea();
        }
        else
        {
        }
    }

    public void ActivarLinea()
    {
        if (laser == null) return;
        ReiniciarLinea();
        laser.enabled = usarLaser;
    }

    public void DesactivarLinea()
    {
        if (laser == null) return;
        ReiniciarLinea();
        laser.enabled = false;
    }

    void ReiniciarLinea()
    {
        if(laser == null) return;
        laser.SetPosition(0, transform.position + offsets[0]);
        laser.SetPosition(1, transform.position + offsets[0]);
    }

    public void Morir()
    {
        particulasMuerte.SetActive(true);
        particulasMuerte.transform.SetParent(null);
        Destroy(particulasMuerte, 5);
        grafico.SetActive(false);
        vidaEnemigo = null;
        DesactivarLinea();
        if (CamaraShake.singleton != null)
        {
            CamaraShake.singleton.Shake(0.5f, 0.6f, 0.01f);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position + Vector3.up * 0.3f, transform.up, rangoAtaque);
    }
#endif
}
