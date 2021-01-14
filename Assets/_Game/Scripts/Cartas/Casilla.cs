using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Casilla : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int bando;
    public NombreCasilla nombreCasilla;
    private void Start()
    {
        ControlUICartas.singleton.AgregarCasilla(this);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DragDrop>().dropeo = true;
            eventData.pointerDrag.GetComponent<Carta>().ActivarComando(this);
            Normalizar();

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GetComponent<UnityEngine.UI.Image>().color = Color.blue;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Normalizar();
        }
    }

    public void Normalizar()
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}
