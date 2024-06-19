using System.Collections.Generic;

public class ResultadoBatalla
{
	public bool Ganada { get; set; }

	public int VidaPerdida { get; set; }

	public int PremioOro { get; set; }

	public int PremioExperiencia { get; set; }

	public List<string> Detalle { get; set; }

	public List<ResultadoAtaque> DetalleAtaques { get; set; }

	public ResultadoBatalla(bool ganada, int vidaPerdida, int premioOro, int premioExperiencia, List<string> detalle, List<ResultadoAtaque> detalleAtaques)
	{
		Ganada = ganada;
		VidaPerdida = vidaPerdida;
		PremioOro = premioOro;
		PremioExperiencia = premioExperiencia;
		Detalle = detalle;
		DetalleAtaques = detalleAtaques;
	}

	public static ResultadoBatalla BatallaGanada(int vidaPerdida, int premioOro, int premioExperiencia, List<string> detalle, List<ResultadoAtaque> detalleAtaques)
	{
		return new ResultadoBatalla(true, vidaPerdida, premioOro, premioExperiencia, detalle, detalleAtaques);
	}

	public static ResultadoBatalla BatallaPerdida(List<string> detalle, List<ResultadoAtaque> detalleAtaques)
	{
		return new ResultadoBatalla(false, 0, 0, 0, detalle, detalleAtaques);
	}
}