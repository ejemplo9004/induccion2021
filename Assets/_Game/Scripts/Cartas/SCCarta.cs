using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Carta", menuName = "Morion/Carta")]
[System.Serializable]
public class SCCarta : ScriptableObject
{
    public string   nombre;
    public Raza     raza;
    public string   descripcion;
    public string   descripcionInstitucional;
    public int      estrellas;
    public int      invocable;
    public int      precio = 15;
    public CampoUso campoUso;

    [Header("Comando")]
    public string   comando;
    public string[] parametros;

    [Header("Imagenes")]
    public Sprite               foto;
    public PerfilGraficoCarta   perfil;
}

[System.Serializable]
public class GrupoTipoCarta
{
    public string Tipo = "Anonimo";
    public SCCarta[] cartas;
}

public enum CampoUso
{
    propio  = 1,
    enemigo = 2,
    ambos   = 0
}
