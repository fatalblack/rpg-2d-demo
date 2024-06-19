using System.Collections.Generic;

public class Recolectable
{
	public int Id { get; set; }

	public string Nombre { get; set; } = string.Empty;

	public List<RecolectableRecurso> Recursos { get; set; } = new List<RecolectableRecurso>();

	public RecolectableCaracteristica Caracteristica { get; set; } = new RecolectableCaracteristica();
}