public static class CreadorPersonajes
{
	// creamos un personaje con los parámetros provistos
	public static Personaje Crear(int idPersonaje, string nombrePersonaje)
	{
		// retornamos el personaje creado
		return new Personaje(idPersonaje, nombrePersonaje);
	}
}