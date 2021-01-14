using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAJugador : MonoBehaviour
{
    public static IAJugador     singleton;

    public List<string>         comandos = new List<string>();
    public SCBarajaMaestra      barajaMaestra;
    List<SCCarta>               listaCartas;
    public GestorRecursos       recursos;
    int ultimoRecurso;
    private void Awake()
    {
        singleton = this;
        recursos = GetComponent<GestorRecursos>();
    }
    void Start()
    {
        Cargar();
        listaCartas = barajaMaestra.GetListaCompleta();
        StartCoroutine(MiLoop());
    }

    // Update is called once per frame
    IEnumerator MiLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int p = Random.Range(0, comandos.Count);
            VerificarRecurso(InvocableDeComando(comandos[p]));
            yield return new WaitUntil(() => recursos.VerificarEstrellas(ultimoRecurso));
            recursos.RestarEstrellas(ultimoRecurso);
            ControlCombate.singleton.IngresarComando(comandos[p]);
        }
    }

    public int InvocableDeComando(string _comando)
    {
        string[] cmds = _comando.Split('|');
        if (cmds.Length > 3)
        {
            return int.Parse(cmds[1]);
        }
        return 0;
    }

    void Guardar()
    {
        string listaStr = "";
        for (int i = 0; i < comandos.Count; i++)
        {
            if (listaStr!="")
            {
                listaStr += "~";
            }
            listaStr += comandos[i];
        }
        PlayerPrefs.SetString("comandosIA", listaStr);
    }

    void Cargar()
    {
        string listaStr = PlayerPrefs.GetString("comandosIA", "");
        string[] cmds = listaStr.Split('~');
        for (int i = 0; i < cmds.Length; i++)
        {
            if(cmds[i].Length > 5) comandos.Add(cmds[i]);
        }
    }

    public void Agregar(string c1, string c2, string c3)
    {

        string comandoNuevo = "invocar|" + c1 + "|" + c2 + "|" + (c3 == "2" ? "1" : "2") + "|2";
        comandos.Add(comandoNuevo);
        if (comandos.Count > 50)
        {
            comandos.RemoveAt(Random.Range(0, comandos.Count));
        }
        Guardar();
    }

    public bool VerificarRecurso(int invc)
    {
        SCCarta cartaInvocable = null;
        for (int i = 0; i < listaCartas.Count; i++)
        {
            if (listaCartas[i].invocable == invc)
            {
                cartaInvocable = listaCartas[i];
                break;
            }
        }
        if (cartaInvocable != null)
        {
            ultimoRecurso = cartaInvocable.estrellas;
            return (recursos.VerificarEstrellas(cartaInvocable.estrellas));
        }
        return false;
    }
}
