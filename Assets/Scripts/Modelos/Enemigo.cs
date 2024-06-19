public class Enemigo
{
	public int Id { get; set; }

	public string Nombre { get; set; }

	public int Nivel { get; set; }

	public int PremioOro { get; set; }

	public int PremioExperiencia { get; set; }

	public EnemigoEstadistica Estadistica { get; set; }

	public Enemigo()
	{
		Estadistica = new EnemigoEstadistica();
	}
}