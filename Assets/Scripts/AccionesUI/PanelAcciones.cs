using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PanelAcciones : MonoBehaviour
{
    // variables públicas
    public List<Sprite> IconosObjetos;

    // variables privadas
    GameObject PanelMenu;
    Canvas VentanaEstadisticas;
    Canvas VentanaInventario;
    Canvas VentanaTienda;
    Canvas VentanaGuia;
    Canvas VentanaEnemigo;
    Canvas VentanaCreditos;
    TextMeshProUGUI DatoFuerza;
    TextMeshProUGUI DatoAgilidad;
    TextMeshProUGUI DatoInteligencia;
    TextMeshProUGUI DatoVitalidad;
    TextMeshProUGUI DatoTala;
    TextMeshProUGUI DatoMineria;
    TextMeshProUGUI DatoPersonaje;
    TextMeshProUGUI DatoOro;
    TextMeshProUGUI DatoVidaActual;
    TextMeshProUGUI DatoExperiencia;
    bool EstadisticasVisible = false;
    bool InventarioVisible = false;
    bool TiendaVisible = false;
    bool GuiaVisible = false;
    bool CreditosVisible = false;

    void Start()
    {
        // obtenemos los componentes
        PanelMenu = gameObject;
        VentanaEstadisticas = PanelMenu.GetComponentsInChildren<Canvas>().First(component => component.CompareTag(Tags.VentanaEstadisticas));
        VentanaInventario = PanelMenu.GetComponentsInChildren<Canvas>().First(component => component.CompareTag(Tags.VentanaInventario));
        VentanaTienda = PanelMenu.GetComponentsInChildren<Canvas>().First(component => component.CompareTag(Tags.VentanaTienda));
        VentanaGuia = PanelMenu.GetComponentsInChildren<Canvas>().First(component => component.CompareTag(Tags.VentanaGuia));
        VentanaEnemigo = PanelMenu.GetComponentsInChildren<Canvas>().First(component => component.CompareTag(Tags.VentanaEnemigo));
        VentanaCreditos = PanelMenu.GetComponentsInChildren<Canvas>().First(component => component.CompareTag(Tags.VentanaCreditos));
        DatoFuerza = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoFuerza));
        DatoAgilidad = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoAgilidad));
        DatoInteligencia = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoInteligencia));
        DatoVitalidad = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoVitalidad));
        DatoTala = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoTala));
        DatoMineria = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoMineria));
        DatoPersonaje = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoPersonaje));
        DatoOro = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoOro));
        DatoVidaActual = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoVidaActual));
        DatoExperiencia = PanelMenu.GetComponentsInChildren<TextMeshProUGUI>().First(component => component.CompareTag(Tags.DatoExperiencia));

        // inicializamos el estado de las ventanas
        ActualizarVistaVentanaEstadisticas();
        ActualizarVistaVentanaInventario();
        ActualizarVistaVentanaTienda();
        ActualizarVistaVentanaGuia();
        ActualizarVistaVentanaEnemigo();
        ActualizarVistaVentanaCreditos();
        ActualizarDatos();
        ActualizarEstadisticas();
    }

    void Update()
    {
        // si el juego no inicio, no realizamos acción alguna
        if (!ValoresGlobales.JuegoIniciado)
        {
            return;
        }

        // actualizamos el estado de las ventanas
        ActualizarDatos();
        ActualizarEstadisticas();
        EstablecerPuedeVender();

        // establecemos las teclas por defecto para las ventanas
        // C para Estadísticas
        if (Input.GetKeyDown(KeyCode.C))
        {
            MostrarEstadisticas();
        }
        // I para Inventario
        if (Input.GetKeyDown(KeyCode.I))
        {
            MostrarInventario();
        }
        // T para Tienda
        if (Input.GetKeyDown(KeyCode.T))
        {
            MostrarTienda();
        }
        // G para Guia
        if (Input.GetKeyDown(KeyCode.G))
        {
            MostrarGuia();
        }
        // U para Créditos
        if (Input.GetKeyDown(KeyCode.U))
        {
            MostrarCreditos();
        }
    }

    // hacemos un switch entre el estado de la ventana de estadísticas (si está abierta se cierra y si está cerrada se abre)
    public void MostrarEstadisticas()
    {
        EstadisticasVisible = !EstadisticasVisible;
        ReproducirAudioVentana(EstadisticasVisible);

        ActualizarVistaVentanaEstadisticas();

    }

    // ocultamos de la ventana de estadísticas
    public void OcultarEstadisticas()
    {
        EstadisticasVisible = false;
        ReproducirAudioVentana(EstadisticasVisible);

        ActualizarVistaVentanaEstadisticas();
    }

    // hacemos un switch entre el estado de la ventana de inventario (si está abierta se cierra y si está cerrada se abre)
    public void MostrarInventario()
    {
        InventarioVisible = !InventarioVisible;
        ReproducirAudioVentana(InventarioVisible);

        ActualizarVistaVentanaInventario();

    }

    // ocultamos un switch entre el estado de la ventana de inventario
    public void OcultarInventario()
    {
        InventarioVisible = false;
        ReproducirAudioVentana(InventarioVisible);

        ActualizarVistaVentanaInventario();
    }

    // hacemos un switch entre el estado de la ventana de la tienda (si está abierta se cierra y si está cerrada se abre)
    public void MostrarTienda()
    {
        TiendaVisible = !TiendaVisible;
        ReproducirAudioVentana(TiendaVisible);

        ActualizarVistaVentanaTienda();

    }

    // ocultamos un switch entre el estado de la ventana de la tienda
    public void OcultarTienda()
    {
        TiendaVisible = false;
        ReproducirAudioVentana(TiendaVisible);

        ActualizarVistaVentanaTienda();
    }

    // hacemos un switch entre el estado de la ventana de la guía (si está abierta se cierra y si está cerrada se abre)
    public void MostrarGuia()
    {
        GuiaVisible = !GuiaVisible;
        ReproducirAudioVentana(GuiaVisible);

        ActualizarVistaVentanaGuia();
    }

    // ocultamos un switch entre el estado de la ventana de la guía
    public void OcultarGuia()
    {
        GuiaVisible = false;
        ReproducirAudioVentana(GuiaVisible);

        ActualizarVistaVentanaGuia();
    }

    // hacemos un switch entre el estado de la ventana de los créditos (si está abierta se cierra y si está cerrada se abre)
    public void MostrarCreditos()
    {
        CreditosVisible = !CreditosVisible;
        ReproducirAudioVentana(CreditosVisible);

        ActualizarVistaVentanaCreditos();
    }

    // ocultamos un switch entre el estado de la ventana de los créditos
    public void OcultarCreditos()
    {
        CreditosVisible = false;
        ReproducirAudioVentana(CreditosVisible);

        ActualizarVistaVentanaCreditos();
    }

    // actualizamos el estado de visibilidad del gameobject de la ventana estadística
    private void ActualizarVistaVentanaEstadisticas()
    {
        VentanaEstadisticas.gameObject.SetActive(EstadisticasVisible);
    }

    // actualizamos el estado de visibilidad del gameobject de la ventana inventario
    private void ActualizarVistaVentanaInventario()
    {
        VentanaInventario.gameObject.SetActive(InventarioVisible);
    }

    // actualizamos el estado de visibilidad del gameobject de la ventana de la tienda
    private void ActualizarVistaVentanaTienda()
    {
        VentanaTienda.gameObject.SetActive(TiendaVisible);
    }

    // actualizamos el estado de visibilidad del gameobject de la ventana de la guía
    private void ActualizarVistaVentanaGuia()
    {
        VentanaGuia.gameObject.SetActive(GuiaVisible);
    }

    // actualizamos el estado de visibilidad del gameobject de la ventana de la guía, por defecto oculta
    private void ActualizarVistaVentanaEnemigo()
    {
        VentanaEnemigo.gameObject.GetComponent<Canvas>().enabled = false;
    }

    // actualizamos el estado de visibilidad del gameobject de la ventana de los créditos
    private void ActualizarVistaVentanaCreditos()
    {
        VentanaCreditos.gameObject.SetActive(CreditosVisible);
    }

    // actualizamos los datos de la barra de estado inferior
    private void ActualizarDatos()
    {
        PersonajeEstadistica estadistica = GameManager.Instance.Jugador.Estadistica;

        DatoPersonaje.text = GameManager.Instance.Jugador.Nombre;
        DatoOro.text = GameManager.Instance.Jugador.Oro.ToString();
        DatoVidaActual.text = $"{estadistica.VidaActualCalculada}/{estadistica.VidaMaximaCalculada}";
        DatoExperiencia.text = ObtenerDatoNivelJugador();
    }

    // actualizamos los datos que contiene la ventada de estadísticas
    private void ActualizarEstadisticas()
    {
        PersonajeEstadistica estadistica = GameManager.Instance.Jugador.Estadistica;

        DatoFuerza.text = "<color=\"white\">FUE</color> <pos=50%><color=\"white\">ATQ</color>\r\n";
        DatoFuerza.text += $"{estadistica.Fuerza} <pos=50%>{estadistica.AtaqueCalculado}";
        DatoAgilidad.text = "<color=\"white\">AGI</color> <pos=50%><color=\"white\">V.ATQ</color>\r\n";
        DatoAgilidad.text += $"{estadistica.Agilidad} <pos=50%>{estadistica.VelocidadAtaqueCalculada}";
        DatoInteligencia.text = "<color=\"white\">INT</color> <pos=50%><color=\"white\">EVA</color>\r\n";
        DatoInteligencia.text += $"{estadistica.Inteligencia} <pos=50%>{estadistica.EvasionCalculada}";
        DatoVitalidad.text = "<color=\"white\">VIT</color> <pos=50%><color=\"white\">VIDA</color>\r\n";
        DatoVitalidad.text += $"{estadistica.Vitalidad} <pos=50%>{estadistica.VidaMaximaCalculada}";
        DatoTala.text = ObtenerDatoNivelTala();
        DatoMineria.text = ObtenerDatoNivelMineria();
    }

    // obtenemos los datos del jugador para cargar la barra inferior de estado
    private string ObtenerDatoNivelJugador()
    {
        int experienciaActual = GameManager.Instance.Jugador.Experiencia;
        int nivelActual = GameManager.Instance.Jugador.Nivel;
        int experienciaMaximaActual = CriterioNiveles.ObtenerListaNiveles()
            .First(nivel => nivel.Nivel == nivelActual).ExperienciaNecesaria;
        int experienciaMaximaSiguiente = CriterioNiveles.ObtenerListaNiveles()
            .FirstOrDefault(nivel => nivel.Nivel == nivelActual + 1)?.ExperienciaNecesaria ?? experienciaMaximaActual;

        return $"Nv. {nivelActual} ({experienciaActual}/{experienciaMaximaSiguiente})";
    }

    // obtenemos los datos del oficio tala, tanto nivel como experiencia actual y experiencia para pasar al siguiente nivel
    private string ObtenerDatoNivelTala()
    {
        PersonajeOficio oficio = GameManager.Instance.Jugador.Oficio;
        int experienciaActual = oficio.TalaExperiencia;
        int nivelActual = oficio.TalaNivel;
        int experienciaMaximaActual = CriterioNiveles.ObtenerListaNiveles()
            .First(nivel => nivel.Nivel == nivelActual).ExperienciaNecesaria;
        int experienciaMaximaSiguiente = CriterioNiveles.ObtenerListaNiveles()
            .FirstOrDefault(nivel => nivel.Nivel == nivelActual + 1)?.ExperienciaNecesaria ?? experienciaMaximaActual;

        return $"<color=\"white\">TALA</color> Nv. {nivelActual}\r\n{experienciaActual}/{experienciaMaximaSiguiente}";
    }

    // obtenemos los datos del oficio minería, tanto nivel como experiencia actual y experiencia para pasar al siguiente nivel
    private string ObtenerDatoNivelMineria()
    {
        PersonajeOficio oficio = GameManager.Instance.Jugador.Oficio;
        int experienciaActual = oficio.MineriaExperiencia;
        int nivelActual = oficio.MineriaNivel;
        int experienciaMaximaActual = CriterioNiveles.ObtenerListaNiveles()
            .First(nivel => nivel.Nivel == nivelActual).ExperienciaNecesaria;
        int experienciaMaximaSiguiente = CriterioNiveles.ObtenerListaNiveles()
            .FirstOrDefault(nivel => nivel.Nivel == nivelActual + 1)?.ExperienciaNecesaria ?? experienciaMaximaActual;

        return $"<color=\"white\">MINA</color> Nv. {nivelActual}\r\n{experienciaActual}/{experienciaMaximaSiguiente}";
    }

    private void EstablecerPuedeVender()
    {
        // si la tienda y el inventario están visibles indicamos que puede vender objetos
        // esto parcialmente suplantaría la función de equipar objetos o consumirlos
        GameManager.Instance.EstablecerPuedeVender(InventarioVisible && TiendaVisible);
    }

    private void ReproducirAudioVentana(bool abrir)
    {
        if (abrir)
        {
            // reproducimos el audio de abrir ventana
            GameManager.Instance.reproductorPanel.ReproducirAbrirVentana();
        }
        else
        {
            // reproducimos el audio de cerrar ventana
            GameManager.Instance.reproductorPanel.ReproducirCerrarVentana();
        }
    }
}