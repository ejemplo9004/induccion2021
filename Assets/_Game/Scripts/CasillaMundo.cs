using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasillaMundo : MonoBehaviour
{
    public NombreCasilla    nombre;
    public bool             renombrar;
    public int              bando;

    private void Start()
    {
        ControlCombate.singleton.AgregarCasillaMundo(this, bando);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one * 0.5f);
    }
    void OnDrawGizmosSelected()
    {
        if (renombrar)
        {
            gameObject.name = nombre.GetNombre();
            renombrar       = false;
        }
    }
}
