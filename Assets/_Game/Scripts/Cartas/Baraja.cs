using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Baraja: MonoBehaviour
{
    public List<ElementoBaraja> cartasDescomprimidas;
    public List<ElementoBaraja> cartasComprimidas;
    public bool simular;
    public string texto;


    public void Comprimir()
    {
        cartasComprimidas = new List<ElementoBaraja>();
        int posi = 0;
        for (int i = 0; i < cartasDescomprimidas.Count; i++)
        {
            posi = BuscarCarta(cartasComprimidas, cartasDescomprimidas[i].tipo, cartasDescomprimidas[i].carta);
            if (posi == -1)
            {
                ElementoBaraja e = new ElementoBaraja();
                e.tipo = cartasDescomprimidas[i].tipo;
                e.carta = cartasDescomprimidas[i].carta;
                e.cantidad = 1;
                cartasComprimidas.Add(e);
            }
            else
            {
                cartasDescomprimidas[posi].cantidad++;
            }
        }
    }

    [ContextMenu("Descomprimir")]
    public void Descomprimir()
    {
        cartasDescomprimidas = new List<ElementoBaraja>();
        foreach (ElementoBaraja elemento in cartasComprimidas)
        {
            for (int i = 0; i < elemento.cantidad; i++)
            {
                cartasDescomprimidas.Add(elemento);
            }
        }
    }

    public int BuscarCarta(List<ElementoBaraja>  bar, TiposBaraja tipo, int carta)
    {
        for (int i = 0; i < bar.Count; i++)
        {
            if (bar[i].tipo == tipo && bar[i].carta == carta)
            {
                return i;
            }
        }
        return -1;
    }

    public void DesdeJson(string json)
    {
        List<ElementoBaraja> b = JsonUtility.FromJson<List<ElementoBaraja>>(json);
        //cartas = b;
    }

    public void Barajar()
    {
        List<ElementoBaraja> cartas2 = new List<ElementoBaraja>();
        int i;
        while (cartasDescomprimidas.Count > 0)
        {
            i = Random.Range(0, cartasDescomprimidas.Count);
            cartas2.Add(cartasDescomprimidas[i]);
            cartasDescomprimidas.RemoveAt(i);
        }
        cartasDescomprimidas = cartas2;
    }

    public void CargarCartas()
    {
        if (!simular)
        {
            texto = ControlGeneral.Cargar("Baraja");
        }
        ConvertirDesdeTexto();
        Descomprimir();
        Barajar();
    }

    [ContextMenu("Guardar")]
    public void GuardarCartas()
    {
        ConvertirATexto();
        if (!simular)
        {
            ControlGeneral.Guardar("Baraja", texto);
        }
    }

    [ContextMenu("A String")]
    public void ConvertirATexto()
    {
        string s = "";
        for (int i = 0; i < cartasComprimidas.Count; i++)
        {
            s += cartasComprimidas[i].ATexto();
            if (i < cartasComprimidas.Count+1)
            {
                s += "|";
            }
        }
        texto = s;
    }

    [ContextMenu("Desde String")]
    public void ConvertirDesdeTexto()
    {
        string[] textos = texto.Split('|');
        cartasComprimidas = new List<ElementoBaraja>();
        for (int i = 0; i < textos.Length; i++)
        {
            if (textos[i].Length > 3)
            {
                ElementoBaraja e = new ElementoBaraja(textos[i]);
                cartasComprimidas.Add(e);
            }
        }
    }
}

[System.Serializable]
public class ElementoBaraja
{
    public TiposBaraja  tipo;
    public int          carta;
    public int          cantidad;

    public string ATexto()
    {
        return (TipoANumero(tipo).ToString() + "!" + carta.ToString() + "!" + cantidad.ToString());
    }
    public ElementoBaraja()
    {
        cantidad = 1;
    }

    public ElementoBaraja(string es)
    {
        DesdeString(es);
    }

    public void DesdeString(string str)
    {
        string[] varios = str.Split('!');
        if (varios.Length >= 3)
        {
            tipo = NumeroATipo(int.Parse(varios[0]));
            carta = int.Parse(varios[1]);
            cantidad = int.Parse(varios[2]);
        }
    }

    public int TipoANumero(TiposBaraja t)
    {
        return (int)t;
    }

    public TiposBaraja NumeroATipo(int n)
    {
        return (TiposBaraja)n;
    }
}