using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventosAcciones : MonoBehaviour
{
    // variables públicas
    public TextMeshProUGUI textoEventos;
    public ScrollRect _scrollerVista;

    public static EventosAcciones Instancia { get; private set; }

    // variables privadas
    List<string> eventos;

    // Start is called before the first frame update
    void Start()
    {
        if (Instancia == null)
        {
            eventos = new List<string>();

            Instancia = this;
        }
    }

    public void AgregarEventoInfo(string evento)
    {
        AgregarEvento(evento, TipoEventos.INFO);
    }

    public void AgregarEventoExito(string evento)
    {
        AgregarEvento(evento, TipoEventos.EXITO);
    }

    public void AgregarEventoPeligro(string evento)
    {
        AgregarEvento(evento, TipoEventos.PELIGRO);
    }

    public void AgregarEventoError(string evento)
    {
        AgregarEvento(evento, TipoEventos.ERROR);
    }

    private void AgregarEvento(string evento, TipoEventos tipo)
    {
        // obtenemos el color correspondiente
        string color = ObtenerColor(tipo);

        // armamos el mensaje de evento
        string mensajeEvento = $"<color=#{color}>{evento}</color>";

        // agregamos el evento a la lista
        eventos.Add(mensajeEvento);

        // mostramos los eventos en el componente text
        ActualizarTextoEventos(mensajeEvento);
    }

    private string ObtenerColor(TipoEventos tipo)
    {
        string color;

        switch (tipo)
        {
            case TipoEventos.EXITO:
                color = "50C956";
                break;
            case TipoEventos.PELIGRO:
                color = "F7A82B";
                break;
            case TipoEventos.ERROR:
                color = "A42A2A";
                break;
            case TipoEventos.INFO:
            default:
                color = "D0D0D0";
                break;
        }

        return color;
    }

    private void ActualizarTextoEventos(string evento)
    {
        // mostramos el mensaje
        textoEventos.text += "\r\n" + evento;

        // movemos el scroll al final
        StartCoroutine(MoverScrollAlFinal());
    }

    private IEnumerator MoverScrollAlFinal()
    {
        yield return new WaitForEndOfFrame();
        
        // movemos el scroll al final
        _scrollerVista.verticalNormalizedPosition = 0;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_scrollerVista.transform);
    }
}