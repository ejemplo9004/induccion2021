using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraShake : MonoBehaviour
{
    public static CamaraShake singleton;
    Vector3 desfase;
    Vector3 posicion;
    void Start()
    {
        singleton = this;
    }

    public void Shake(float tiempo, float magnitud, float periodo)
    {
        StartCoroutine(Shaking(tiempo, magnitud, periodo));
    }

    IEnumerator Shaking(float tiempo, float magnitud, float periodo)
    {
        posicion = transform.position;
        float tiempoFin = Time.time + tiempo;
        while (Time.time < tiempoFin)
        {
            desfase = new Vector3(Random.Range(-magnitud, magnitud), Random.Range(-magnitud, magnitud), Random.Range(-magnitud, magnitud));
            transform.position = posicion + desfase;
            yield return new WaitForSeconds(periodo);
        }
        transform.position = posicion;
    }
}
