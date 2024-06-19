public static class CreadorEnemigos
{
	public static Enemigo CrearPorIdEnemigo(int idEnemigo, int nivel)
	{
		// inicializamos el enemigo vacío por defecto
		Enemigo enemigo = new Enemigo();

		// dependiendo el idEnemigo que hayamos recibido crearemos el indicado
		switch (idEnemigo)
		{
			case (int)EnemigoIndices.EsqueletoSirviente:
				enemigo = CrearEsqueletoSirviente(nivel);
				break;
			case (int)EnemigoIndices.EsqueletoGuerrero:
				enemigo = CrearEsqueletoGuerrero(nivel);
				break;
			case (int)EnemigoIndices.EsqueletoMago:
				enemigo = CrearEsqueletoMago(nivel);
				break;
			case (int)EnemigoIndices.Dragon:
				enemigo = CrearDragon(nivel);
				break;
		}

		// retornamos el enemigo creado
		return enemigo;
	}

	public static Enemigo CrearEsqueletoSirviente(int nivel)
	{
		SolicitudEnemigo solicitud = new SolicitudEnemigo
		{
			IdEnemigo = (int)EnemigoIndices.EsqueletoSirviente,
			Nombre = "Esqueleto Sirviente",
			Nivel = nivel,
			PremioOroBase = 200,
			PremioExperienciaBase = 100,
			FuerzaBase = 20,
			AgilidadBase = 8,
			InteligenciaBase = 8,
			VitalidadBase = 10
		};

		// creamos el Enemigo con los datos específicos y las estadísticas calculadas
		Enemigo enemigo = ConfiguracionInicial(solicitud);

		// retornamos el enemigo
		return enemigo;
	}

	public static Enemigo CrearEsqueletoGuerrero(int nivel)
	{
		SolicitudEnemigo solicitud = new SolicitudEnemigo
		{
			IdEnemigo = (int)EnemigoIndices.EsqueletoGuerrero,
			Nombre = "Esqueleto Guerrero",
			Nivel = nivel,
			PremioOroBase = 300,
			PremioExperienciaBase = 150,
			FuerzaBase = 30,
			AgilidadBase = 10,
			InteligenciaBase = 10,
			VitalidadBase = 15
		};

		// creamos el Enemigo con los datos específicos y las estadísticas calculadas
		Enemigo enemigo = ConfiguracionInicial(solicitud);

		// retornamos el enemigo
		return enemigo;
	}

	public static Enemigo CrearEsqueletoMago(int nivel)
	{
		SolicitudEnemigo solicitud = new SolicitudEnemigo
		{
			IdEnemigo = (int)EnemigoIndices.EsqueletoMago,
			Nombre = "Esqueleto Mago",
			Nivel = nivel,
			PremioOroBase = 500,
			PremioExperienciaBase = 250,
			FuerzaBase = 10,
			AgilidadBase = 35,
			InteligenciaBase = 35,
			VitalidadBase = 15
		};

		// creamos el Enemigo con los datos específicos y las estadísticas calculadas
		Enemigo enemigo = ConfiguracionInicial(solicitud);

		// retornamos el enemigo
		return enemigo;
	}

	public static Enemigo CrearDragon(int nivel)
	{
		SolicitudEnemigo solicitud = new SolicitudEnemigo
		{
			IdEnemigo = (int)EnemigoIndices.Dragon,
			Nombre = "Dragón",
			Nivel = nivel,
			PremioOroBase = 1500,
			PremioExperienciaBase = 750,
			FuerzaBase = 100,
			AgilidadBase = 100,
			InteligenciaBase = 100,
			VitalidadBase = 100
		};

		// creamos el Enemigo con los datos específicos y las estadísticas calculadas
		Enemigo enemigo = ConfiguracionInicial(solicitud);

		// retornamos el enemigo
		return enemigo;
	}

	private static Enemigo ConfiguracionInicial(SolicitudEnemigo solicitud)
	{
		Enemigo enemigo = new Enemigo();

		// establecemos los stats para el nivel correspondiente
		int premioOro = solicitud.PremioOroBase * solicitud.Nivel;
		int premioExperiencia = solicitud.PremioExperienciaBase * solicitud.Nivel;

		// establecemos los stats para el nivel correspondiente
		int fuerza = solicitud.FuerzaBase * solicitud.Nivel;
		int agilidad = solicitud.AgilidadBase * solicitud.Nivel;
		int inteligencia = solicitud.InteligenciaBase * solicitud.Nivel;
		int vitalidad = solicitud.VitalidadBase * solicitud.Nivel;

		// calculamos y retornamos las estadísticas del enemigo en base a los 4 stats principales (fuerza, agilidad, inteligencia y vitalidad)
		EntidadEstadisticaBase estadisticaBase = Calculos.CalcularEstadisticasBase(fuerza, agilidad, inteligencia, vitalidad, solicitud.Nivel);
		EnemigoEstadistica estadistica = new EnemigoEstadistica(solicitud.IdEnemigo, estadisticaBase);
		enemigo.Id = solicitud.IdEnemigo;
		enemigo.Nombre = solicitud.Nombre;
		enemigo.Nivel = solicitud.Nivel;
		enemigo.PremioOro = premioOro;
		enemigo.PremioExperiencia = premioExperiencia;
		enemigo.Estadistica = estadistica;

		// retornamos el enemigo creado
		return enemigo;
	}
}