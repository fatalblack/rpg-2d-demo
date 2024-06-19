public static class PersonajeAcciones
{
    private static Personaje _personaje = GameManager.Instance.Jugador;
    private static PersonajeEstadistica _personajeEstadistica = GameManager.Instance.Jugador.Estadistica;

    public static void IncrementarOro(int oro)
    {
        // incrementamos la cantidad de oro
        _personaje.Oro += oro;
    }

    public static void RestarOro(int oro)
    {
        // restamos la cantidad de oro
        _personaje.Oro -= oro;
    }

    public static void IncrementarExperiencia(int experiencia)
    {
        // incrementamos la experiencia
        _personaje.Experiencia += experiencia;

        // calcula nuevamente el nivel actual del personaje
        int nivelActualizado = CriterioNiveles.ObtenerNivelPorExperiencia(_personaje.Experiencia);

        // si el nivel es mayor al que tiene el personaje lo actualizamos y las estadísticas también
        if (_personaje.Nivel < nivelActualizado)
        {
            _personaje.Nivel = nivelActualizado;

            PersonajeEstadisticaAcciones.ActualizarEstadisticas();

            // informamos al visor de eventos que subió de nivel el personaje
            EventosAcciones.Instancia.AgregarEventoExito($"Subiste a nivel {nivelActualizado}");
        }
    }

    public static void CurarVida(int vidaCurada)
    {
        // actualizamos la cantidad de vida
        _personajeEstadistica.VidaActualCalculada += vidaCurada;

        // mantenemos la vida al máximo posible sin que sobrepase el mismo
        if (_personajeEstadistica.VidaActualCalculada > _personajeEstadistica.VidaMaximaCalculada)
        {
            _personajeEstadistica.VidaActualCalculada = _personajeEstadistica.VidaMaximaCalculada;
        }
    }

    public static void DanarVida(int vidaDanada)
    {
        // actualizamos la cantidad de vida
        _personajeEstadistica.VidaActualCalculada -= vidaDanada;

        // mantenemos la vida como mínimo en 0 para que no quede en negativo
        if (_personajeEstadistica.VidaActualCalculada < 0)
        {
            _personajeEstadistica.VidaActualCalculada = 0;
        }
    }

    public static void EstablecerVida(int vida)
    {
        // actualizamos la cantidad de vida
        _personajeEstadistica.VidaActualCalculada = vida < 0 ? 0 : vida;
    }
}