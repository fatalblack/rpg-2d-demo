public static class PersonajeOficioAcciones
{
    private static PersonajeOficio _personajeOficio = GameManager.Instance.Jugador.Oficio;

    public static void ActualizarTala(int experiencia)
    {
        // incrementamos la experiencia de tala
        _personajeOficio.TalaExperiencia += experiencia;
        // recalculamos el nivel actual del oficio
        int nuevoNivelTala = CriterioNiveles.ObtenerNivelPorExperiencia(_personajeOficio.TalaExperiencia);

        // si el nivel actual es menor que el calculado, lo subimos
        if (_personajeOficio.TalaNivel < nuevoNivelTala)
        {
            // subimos el nivel de tala
            _personajeOficio.TalaNivel = nuevoNivelTala;

            // informamos al visor de eventos que subió de nivel el oficio
            EventosAcciones.Instancia.AgregarEventoExito($"Subiste tala a nivel {nuevoNivelTala}");
        }
    }

    public static void ActualizarMineria(int experiencia)
    {
        // incrementamos la experiencia de minería
        _personajeOficio.MineriaExperiencia += experiencia;
        // recalculamos el nivel actual del oficio
        int nuevoNivelMina = CriterioNiveles.ObtenerNivelPorExperiencia(_personajeOficio.MineriaExperiencia);

        // si el nivel actual es menor que el calculado, lo subimos
        if (_personajeOficio.MineriaNivel < nuevoNivelMina)
        {
            // subimos el nivel de minería
            _personajeOficio.MineriaNivel = nuevoNivelMina;

            // informamos al visor de eventos que subió de nivel el oficio
            EventosAcciones.Instancia.AgregarEventoExito($"Subiste minería a nivel {nuevoNivelMina}");
        }
    }
}