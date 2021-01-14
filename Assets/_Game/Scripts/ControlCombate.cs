using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCombate : MonoBehaviour
{
    public static ControlCombate singleton;
    [Header("Debugging")]
    public bool debuguearCartas;

    [Header("General")]
    public Combatiente jugador1;
    public Combatiente jugador2;
    public List<CasillaMundo> casillasMundo1;
    public List<CasillaMundo> casillasMundo2;
    public GestorRecursos[] gestoresRecursos;
    public GameObject[] particulasCreacion;
    public float tiempoEsperaInvocacion = 1;

    public SCBarajaMaestra barajaMaestra;


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

    ///----------------------> Monstruos

    public void AgregarMonstruo(Monstruo m, int bando)
    {
        if (bando == 1)
        {
            jugador1.monstruos.Add(m);
        }
        else
        {
            jugador2.monstruos.Add(m);
        }
    }
    public void QuitarMonstruo(Monstruo m, int bando)
    {
        if (bando == 1)
        {
            jugador1.monstruos.Remove(m);
        }
        else
        {
            jugador2.monstruos.Remove(m);
        }
    }

    public List<Monstruo> GetMonstruosEnemigos(int bando)
    {
        if (bando == 1)
        {
            return jugador2.monstruos;
        }
        return jugador1.monstruos;
    }

    ///----------------------> Torres
    public void AgregarTorre(Torre m, int bando)
    {
        if (bando == 1)
        {
            jugador1.torres.Add(m);
        }
        else
        {
            jugador2.torres.Add(m);
        }
    }
    public void QuitarTorre(Torre m, int bando)
    {
        if (bando == 1)
        {
            jugador1.torres.Remove(m);
        }
        else
        {
            jugador2.torres.Remove(m);
        }
    }

    public List<Torre> GetTorresEnemigos(int bando)
    {
        if (bando == 1)
        {
            return jugador2.torres;
        }
        return jugador1.torres;
    }

    /// ---------------------> Casillas
    public void AgregarCasillaMundo(CasillaMundo c, int bando)
    {
        GetListaCasillas(bando).Add(c);
    }

    public CasillaMundo GetCasillaMundo(string nombre, int bando)
    {
        List<CasillaMundo> lcm = GetListaCasillas(bando);
        for (int i = 0; i < lcm.Count; i++)
        {
            if (lcm[i].nombre.Comparar(nombre))
            {
                return lcm[i];
            }
        }
        return null;
    }

    List<CasillaMundo> GetListaCasillas(int bando)
    {
        if (bando == 1)
        {
            return casillasMundo1;
        }
        return casillasMundo2;
    }

    public void Instanciar(int criatura, int bando, CasillaMundo casillaMundo)
    {
        if (casillaMundo == null)
        {
            print("Error al buscar Casilla");
        }
        GameObject go = Instantiate(barajaMaestra.invocables[criatura], casillaMundo.transform.position, casillaMundo.transform.rotation) as GameObject;
        Monstruo m = go.GetComponent<Monstruo>();
        m.AsignarBando(bando);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////                                                    //////////////////////
    ////////////////////             ojo por ojo, acá comienza              //////////////////////
    ////////////////////             el intérprete de comandos              //////////////////////
    ////////////////////                                                    //////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////
    

    public void IngresarComando(string comando)
    {
        if(ControlGeneral.singleton != null && ControlGeneral.singleton.debuguearTodo) 
            print("<color=blue> Comando llamado </color>" + comando);
        string[] comandos = comando.Split('|');
        if (comandos.Length < 1)
        {
            return;
        }
        switch (comandos[0])
        {
            case "invocar":
                if (comandos.Length < 5)
                {
                    return;
                }
                Invocar(comandos[1], comandos[2], comandos[3], comandos[4]);
                if (IAJugador.singleton != null)
                {
                    IAJugador.singleton.Agregar(comandos[1], comandos[2], comandos[3]);
                }
                break;
            default:
                break;
        }
    }

    void Invocar(string invocable, string casilla, string bandoInvocaion, string bandoInvocador)
    {
        int _invocable = int.Parse(invocable);
        int _bandoInvocaion = int.Parse(bandoInvocaion);
        int _bandoInvocador = int.Parse(bandoInvocador);
        CasillaMundo cm = GetCasillaMundo(casilla, _bandoInvocaion);
        StartCoroutine(EsperarInvocacion(_invocable, _bandoInvocador, cm, tiempoEsperaInvocacion));
        //print(_bandoInvocador - 1);
        print(casilla);
        Instantiate(particulasCreacion[_bandoInvocador-1], cm.transform.position + Vector3.up*0.3f, Quaternion.identity);
    }

    IEnumerator EsperarInvocacion(int _invocable, int _bandoInvocador, CasillaMundo cm, float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);
        Instanciar(_invocable, _bandoInvocador, cm);
    }

}

[System.Serializable]
public class Combatiente
{
    public List<Monstruo>   monstruos   = new List<Monstruo>();
    public List<Torre>      torres      = new List<Torre>();
    public PuestoControl    puestoControl;
}