public class EnemigoEstadistica : EntidadEstadisticaBase
{
	public int IdEnemigo { get; set; }

	public EnemigoEstadistica()
	{
	}

	public EnemigoEstadistica(int idEnemigo, EntidadEstadisticaBase estadisticaBase)
	{
		IdEnemigo = idEnemigo;
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