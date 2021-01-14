using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Carta))]
[RequireComponent(typeof(CanvasGroup))]
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas           canvas;
    public CanvasGroup      canvasGroup;
    private Carta           miCarta;
    private RectTransform   rt;

    public bool             dragging;
    public bool             dropeo;
    public static float     tiempoBloqueoArrastre;

    Vector2 posicionInicial;
    public Transform padre;
    float viejaPosManoY;

    private void Awake()
    {
        miCarta = GetComponent<Carta>();
        rt = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        posicionInicial = rt.anchoredPosition;
        padre = transform.parent;
        viejaPosManoY = padre.transform.position.y;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (miCarta.EsDrageable() && tiempoBloqueoArrastre < Time.time)
        {
            dragging = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        VerificarDrop();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            ControlUICartas.singleton.OcultarTodo();
            MostrarrMano();
            StartCoroutine(VerificarDrop());
        }
    }

    IEnumerator VerificarDrop()
    {
        yield return new WaitForSeconds(0.2f);
        if (!dropeo)
        {
            transform.parent = null;
            ReiniciarPosicion();
        }
        else
        {
            tiempoBloqueoArrastre = Time.time + 1;
            LeanTween.scale(gameObject, Vector3.one*2, 1).setDestroyOnComplete(true).setEase(LeanTweenType.easeOutExpo); /////////////////////////////// Temporal
            LeanTween.alpha(rt, 0, 1).setEase(LeanTweenType.easeOutExpo);
            ControlUICartas.singleton.delegadoJaladas();
        }
    }

    void ReiniciarPosicion()
    {
        transform.parent = padre;
        //rt.anchoredPosition = posicionInicial;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            transform.parent = canvas.transform;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.5f;
            ControlUICartas.singleton.MostrarTodo(miCarta.uso);
            OcultarMano();
        }
    }

    void OcultarMano()
    {
        LeanTween.moveY(padre.gameObject, -70, 0.2f);
    }
    void MostrarrMano()
    {
        LeanTween.moveY(padre.gameObject, viejaPosManoY, 0.2f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        
    }
}
