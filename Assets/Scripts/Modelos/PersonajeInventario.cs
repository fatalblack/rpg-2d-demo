public class PersonajeInventario
{
	public int Id { get; set; }

	public int IdPersonaje { get; set; }

	public int IdObjeto { get; set; }

	public bool Equipado { get; set; }

	public int Cantidad { get; set; }

	public Objeto Objeto { get; set; }

	public PersonajeInventario()
	{
		Objeto = new Objeto();
	}
}