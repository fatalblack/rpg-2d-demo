using System.Collections.Generic;

public static class PersonajeEstadisticaAcciones
{
    private static Personaje _personaje = GameManager.Instance.Jugador;
    private static PersonajeEstadistica _personajeEstadistica = GameManager.Instance.Jugador.Estadistica;
    private static List<PersonajeInventario> _personajeInventario = GameManager.Instance.Jugador.Inventario;

    public static ObjetoEstadistica ObtenerEstadisticaTotalEquipados()
    {
        // inicializamos las estadísticas en 0
        ObjetoEstadistica estadisticaTotalEquipados = new ObjetoEstadistica();

        // en caso de que se llegara a poner un objeto equipado en el inventario por defecto
        // se deberían acumular las estadísticas
        if (_personajeInventario.Count > 0)
        {
            for (int i = 0; i < _personajeInventario.Count; i++)
            {
                PersonajeInventario objetoInventario = _personajeInventario[i];
                Objeto objeto = objetoInventario.Objeto;

                // por cada objeto equipado incrementamos las estadísticas principales
                if (objetoInventario.Equipado)
                {
                    estadisticaTotalEquipados.Fuerza += objeto.Estadistica.Fuerza;
                    estadisticaTotalEquipados.Agilidad += objeto.Estadistica.Agilidad;
                    estadisticaTotalEquipados.Inteligencia += objeto.Estadistica.Inteligencia;
                    estadisticaTotalEquipados.Vitalidad += objeto.Estadistica.Vitalidad;
                }
            }
        }

        // retornamos las estadísticas ya sumadas
        return estadisticaTotalEquipados;
    }

    public static PersonajeEstadistica ActualizarEstadisticas()
    {
        // obtenemos las estadísticas que nos dan los objetos equipados
        ObjetoEstadistica estadisticaTotalEquipados = ObtenerEstadisticaTotalEquipados();
        EntidadEstadisticaBase calculoEstadisticaObjeto = Calculos.CalcularEstadisticasEquipable(estadisticaTotalEquipados);
        // obtenemos las estadísticas base del personaje por nivel
        EntidadEstadisticaBase calculoEstadisticaPersonaje = Calculos.CalcularEstadisticasBase(
            ConfiguracionPersonaje.FuerzaBase,
            ConfiguracionPersonaje.AgilidadBase,
            ConfiguracionPersonaje.InteligenciaBase,
            ConfiguracionPersonaje.VitalidadBase,
            _personaje.Nivel);

        // sumamos las estadísticas de equipados y base
        _personajeEstadistica.Fuerza = calculoEstadisticaObjeto.Fuerza + calculoEstadisticaPersonaje.Fuerza;
        _personajeEstadistica.Agilidad = calculoEstadisticaObjeto.Agilidad + calculoEstadisticaPersonaje.Agilidad;
        _personajeEstadistica.Inteligencia = calculoEstadisticaObjeto.Inteligencia + calculoEstadisticaPersonaje.Inteligencia;
        _personajeEstadistica.Vitalidad = calculoEstadisticaObjeto.Vitalidad + calculoEstadisticaPersonaje.Vitalidad;
        _personajeEstadistica.AtaqueCalculado = calculoEstadisticaObjeto.AtaqueCalculado + calculoEstadisticaPersonaje.AtaqueCalculado;
        _personajeEstadistica.VelocidadAtaqueCalculada = calculoEstadisticaObjeto.VelocidadAtaqueCalculada + calculoEstadisticaPersonaje.VelocidadAtaqueCalculada;
        _personajeEstadistica.EvasionCalculada = calculoEstadisticaObjeto.EvasionCalculada + calculoEstadisticaPersonaje.EvasionCalculada;
        _personajeEstadistica.VidaMaximaCalculada = calculoEstadisticaObjeto.VidaMaximaCalculada + calculoEstadisticaPersonaje.VidaMaximaCalculada;
        _personajeEstadistica.VidaActualCalculada = calculoEstadisticaObjeto.VidaActualCalculada + calculoEstadisticaPersonaje.VidaActualCalculada;

        // retornamos las estadísticas
        return _personajeEstadistica;
    }
}