using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlGeneral : MonoBehaviour
{
    public static ControlGeneral singleton;
    [Header("Debugging")]
    public bool debuguearTodo;
    public int pascualCoins;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public int GetPascualCoins()
    {
        pascualCoins = PlayerPrefs.GetInt("PascualCoins");
        return pascualCoins;
    }
    public void SetPascualCoins(int cuantos)
    {
        pascualCoins = cuantos;
        PlayerPrefs.SetInt("PascualCoins", pascualCoins);
    }

    public void SumarPascualCoins(int cuantosSumar)
    {
        pascualCoins += cuantosSumar;
        SetPascualCoins(pascualCoins);
    }

    public void RestarPascualCoins(int cuantosRestar)
    {
        pascualCoins -= cuantosRestar;
        if (pascualCoins<0)
        {
            pascualCoins = 0;
        }
        SetPascualCoins(pascualCoins);
    }

    public static void Guardar(string llave, string valor)
    {
        PlayerPrefs.SetString(llave, valor);
    }

    public static string Cargar(string llave)
    {
        return PlayerPrefs.GetString(llave, "");
    }

    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        Application.Quit();
    }
}

[System.Serializable]
public enum Estados
{
    idle        = 0,
    siguiendo   = 1,
    atacando    = 2,
    muerto      = 3,
    paralizado  = 4
}

[System.Serializable]
public enum Raza
{
    Orco        = 0,
    Elfo        = 1,
    Humano      = 2,
    Artefacto   = 3,
    Conjuro     = 4
}

[System.Serializable]
public enum Clase
{
    Guerrero    = 0,
    Arquero     = 1,
    Mago        = 2,
    NoAplica    = 3,
    Torre       = 4
}

[System.Serializable]
public class Stats
{
    [Header("Estadisticas Principales")]
    public float velocidad;
    public float salud;
    public float fuerza;
    public float resistencia;
    public float periodoDaño;
    public float rangoAtaque;
    public float rangoVision;
}

[System.Serializable]
public enum Busqueda
{
    Monstruos       = 0,
    Torres          = 1,
    PuestosControl  = 2
}

[System.Serializable]
public class NombreCasilla
{
    public string Letra;
    public int numero;

    public bool Comparar(string cas)
    {
        string s = Letra.ToUpper() + numero;
        return cas.Equals(s);
    }

    public string GetNombre()
    {
        return Letra.ToUpper() + numero;
    }
}