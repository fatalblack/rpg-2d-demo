public class ResultadoAtaque
{
	public bool EsTurnoJugador { get; set; }

	public int DanioAplicado { get; set; }

	public bool Evadido { get; set; }

	public ResultadoAtaque(bool esTurnoJugador, int danioAplicado, bool evadido)
	{
		EsTurnoJugador = esTurnoJugador;
		DanioAplicado = danioAplicado;
		Evadido = evadido;
	}
}