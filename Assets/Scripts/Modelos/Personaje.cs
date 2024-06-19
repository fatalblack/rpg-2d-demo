using System.Collections.Generic;

public class Personaje
{
	public int Id { get; set; }

	public string Nombre { get; set; }

	public int Experiencia { get; set; }

	public int Nivel { get; set; }

	public int Oro { get; set; }

	public PersonajeEstadistica Estadistica { get; set; }

	public PersonajeOficio Oficio { get; set; }

	public List<PersonajeInventario> Inventario { get; set; }

	public Personaje(int idPersonaje, string nombre)
	{
		Id = idPersonaje;
		Nombre = nombre;
		Experiencia = 0;
		Nivel = CriterioNiveles.ObtenerNivelPorExperiencia(Experiencia);
		Oro = 100000;

		Estadistica = new PersonajeEstadistica(
			Id,
			Calculos.CalcularEstadisticasBase(
				ConfiguracionPersonaje.FuerzaBase,
				ConfiguracionPersonaje.AgilidadBase,
				ConfiguracionPersonaje.InteligenciaBase,
				ConfiguracionPersonaje.VitalidadBase,
				Nivel));

		Oficio = new PersonajeOficio
		{
			IdPersonaje = idPersonaje,
			TalaExperiencia = 0,
			TalaNivel = CriterioNiveles.ObtenerNivelPorExperiencia(0),
			MineriaExperiencia = 0,
			MineriaNivel = CriterioNiveles.ObtenerNivelPorExperiencia(0)
		};

		Inventario = new List<PersonajeInventario>();
	}
}