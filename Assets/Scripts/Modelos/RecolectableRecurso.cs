public class RecolectableRecurso
{
	public int IdRecolectable { get; set; }

	public int IdObjeto { get; set; }

	public int Experiencia { get; set; }

	public int Cantidad { get; set; }

	public int Probabilidad { get; set; }

	public Objeto Objeto { get; set; } = new Objeto();
}