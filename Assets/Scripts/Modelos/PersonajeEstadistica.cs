public class PersonajeEstadistica : EntidadEstadisticaBase
{
	public int IdPersonaje { get; set; }

	public PersonajeEstadistica()
	{
	}

	public PersonajeEstadistica(int idPersonaje, EntidadEstadisticaBase estadisticaBase)
	{
		IdPersonaje = idPersonaje;
		Fuerza = estadisticaBase.Fuerza;
		Agilidad = estadisticaBase.Agilidad;
		Inteligencia = estadisticaBase.Inteligencia;
		Vitalidad = estadisticaBase.Vitalidad;
		AtaqueCalculado = estadisticaBase.AtaqueCalculado;
		VelocidadAtaqueCalculada = estadisticaBase.VelocidadAtaqueCalculada;
		EvasionCalculada = estadisticaBase.EvasionCalculada;
		VidaMaximaCalculada = estadisticaBase.VidaMaximaCalculada;
		VidaActualCalculada = estadisticaBase.VidaActualCalculada;
	}
}