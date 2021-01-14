using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestorRecursos : MonoBehaviour
{
    public float    velCargaRecurso = 5;
    public float    recursos;
    public int      estrellas;

    public Slider   slRecursos;
    public Text     txtEstrellas;

    void FixedUpdate()
    {
        recursos += velCargaRecurso * Time.fixedDeltaTime;
        if (recursos>=100)
        {
            estrellas++;
            recursos = 0;
            if (txtEstrellas != null)
            {
                txtEstrellas.text = estrellas.ToString("00");
            }
        }
        if (slRecursos != null)
        {
            slRecursos.value = recursos / 100f;
        }
    }

    public bool VerificarEstrellas(int cuanto)
    {
        return cuanto <= estrellas;
    }

    public void RestarEstrellas(int cuanto)
    {
        estrellas -= cuanto;
        if (txtEstrellas != null)
        {
            txtEstrellas.text = estrellas.ToString("00");
        } 
    }
}
