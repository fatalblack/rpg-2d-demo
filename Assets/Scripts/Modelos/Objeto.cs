public class Objeto
{
	public int Id { get; set; }

	public string Nombre { get; set; }

	public string Descripcion { get; set; }

	public string RutaImagen { get; set; }

	public ObjetoCaracteristica Caracteristica { get; set; }

	public ObjetoEstadistica Estadistica { get; set; }

	public Objeto()
	{
		Nombre = string.Empty;
		Descripcion = string.Empty;
		RutaImagen = string.Empty;
		Caracteristica = new ObjetoCaracteristica();
		Estadistica = new ObjetoEstadistica();
	}
}