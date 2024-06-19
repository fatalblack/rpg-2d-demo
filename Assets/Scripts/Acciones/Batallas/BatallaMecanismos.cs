using System.Collections.Generic;

public static class BatallaMecanismos
{
    private static Personaje _personaje = GameManager.Instance.Jugador;
    
    public static ResultadoBatalla Luchar(Enemigo enemigo, bool simular)
    {
        // icializamos el resultado de la batalla
        ResultadoBatalla resultadoBatalla;
        List<string> resultadoBatallaDetalle = new List<string>();
        List<ResultadoAtaque> resultadoBatallaDetalleAtaque = new List<ResultadoAtaque>();

        // guardamos en un auxiliar la vida actual del personaje para modificarla después
        int vidaPerdida = 0;

        // determinamos quien ataca primero
        bool turnoPersonaje = _personaje.Estadistica.VelocidadAtaqueCalculada >= enemigo.Estadistica.VelocidadAtaqueCalculada;

        // se ejecuta la batalla mientras ambos adversarios tengan vida
        while (vidaPerdida < _personaje.Estadistica.VidaActualCalculada && enemigo.Estadistica.VidaActualCalculada > 0)
        {
            // turno del personaje
            if (turnoPersonaje)
            {
                // realizamos el ataque
                ResultadoAtaque resultadoAtaque = Atacar(true, enemigo.Estadistica, _personaje.Estadistica);

                // actualizamos la vida actual del defensor con el resultado del ataque
                // si se evadió el daño aplicado es 0
                enemigo.Estadistica.VidaActualCalculada -= resultadoAtaque.DanioAplicado;

                // agregamos detalle de la batalla
                if (resultadoAtaque.Evadido)
                {
                    resultadoBatallaDetalle.Add("Atacaste a " + enemigo.Nombre + ". Evadió tu ataque.");
                }
                else
                {
                    resultadoBatallaDetalle.Add("Atacaste a " + enemigo.Nombre + ". Daño causado " + resultadoAtaque.DanioAplicado +".");
                }

                resultadoBatallaDetalleAtaque.Add(resultadoAtaque);
            }
            // turno del enemigo
            else
            {
                // realizamos el ataque
                ResultadoAtaque resultadoAtaque = Atacar(false, _personaje.Estadistica, enemigo.Estadistica);

                // actualizamos la vida actual del defensor con el resultado del ataque
                // si se evadió el daño aplicado es 0
                vidaPerdida += resultadoAtaque.DanioAplicado;

                // agregamos detalle de la batalla
                if (resultadoAtaque.Evadido)
                {
                    resultadoBatallaDetalle.Add("Te atacó " + enemigo.Nombre + ". Evadiste su ataque.");
                }
                // si logramos acertar el ataque descontamos nuestra vida
                else
                {
                    resultadoBatallaDetalle.Add("Te atacó " + enemigo.Nombre + ". Daño causado " + resultadoAtaque.DanioAplicado + ".");
                }

                resultadoBatallaDetalleAtaque.Add(resultadoAtaque);
            }

            // cambiamos el turno
            turnoPersonaje = !turnoPersonaje;
        }

        // preparamos el resultado
        // si finalizamos con vida superior a 0 significa que ganamos
        if (vidaPerdida < _personaje.Estadistica.VidaActualCalculada)
        {
            resultadoBatalla = ResultadoBatalla.BatallaGanada(vidaPerdida, enemigo.PremioOro, enemigo.PremioExperiencia, resultadoBatallaDetalle, resultadoBatallaDetalleAtaque);
        }
        // si finalizamos con vida inferior o igual a 0 significa que perdimos
        else
        {
            resultadoBatalla = ResultadoBatalla.BatallaPerdida(resultadoBatallaDetalle, resultadoBatallaDetalleAtaque);
        }

		// aplicamos el resultado, solo si no es una simulación
		if (!simular)
		{
            AplicarResultadoAlPersonaje(resultadoBatalla);
        }

        // retornamos el resultado
        return resultadoBatalla;
    }

    private static ResultadoAtaque Atacar(bool esTurnoJugador, EntidadEstadisticaBase estadisticaDefensor, EntidadEstadisticaBase estadisticaAtacante)
    {
        // calculamos si se evadió el ataque o no
        bool esAtaqueEvadido = Calculos.EsAtaqueEvadido(estadisticaDefensor.EvasionCalculada);

        // si se evadió no tiene sentido seguir con los cálculos, retornamos ataque esquivado
        if (esAtaqueEvadido)
        {
            return new ResultadoAtaque(esTurnoJugador, 0, true);
        }

        // calculamos el ataque que mitigará el defensor
        int ataqueMitigadoEnemigo = Calculos.CalcularAtaqueMitigado(estadisticaDefensor.Fuerza, estadisticaDefensor.Vitalidad);
        // calculamos el ataque que hará el atacante luego de la mitigación de daño
        int ataqueFinalPersonaje = Calculos.CalcularAtaqueFinal(estadisticaAtacante.AtaqueCalculado, estadisticaAtacante.Agilidad, ataqueMitigadoEnemigo);

        // retornamos el resultado del ataque
        return new ResultadoAtaque(esTurnoJugador, ataqueFinalPersonaje, false);
    }

    public static void AplicarResultadoAlPersonaje(ResultadoBatalla resultado)
    {
        // si ganamos le damos la recompensa al personaje
        // actualizamos la vida que le quedó al personaje
        if (resultado.Ganada)
        {
            PersonajeAcciones.IncrementarOro(resultado.PremioOro);
            PersonajeAcciones.IncrementarExperiencia(resultado.PremioExperiencia);
            PersonajeAcciones.DanarVida(resultado.VidaPerdida);
        }
		else
		{
            // dejamos la vida del personaje en 0
            PersonajeAcciones.EstablecerVida(0);
        }
    }



    public static void InformarEventoAtaque(ResultadoAtaque resultadoAtaque)
    {
        string mensaje;

        // cuando el jugador ataca
        if (resultadoAtaque.EsTurnoJugador)
        {
            if (resultadoAtaque.Evadido)
            {
                mensaje = $"Atacaste a {GameManager.Instance.EnemigoActual.Nombre}. Evadió tu ataque.";
            }
            else
            {
                mensaje = $"Atacaste a {GameManager.Instance.EnemigoActual.Nombre}. Daño causado {resultadoAtaque.DanioAplicado}.";
            }
        }
        // cuando el enemigo ataca
        else
        {
            if (resultadoAtaque.Evadido)
            {
                mensaje = $"Te atacó {GameManager.Instance.EnemigoActual.Nombre}. Evadiste su ataque.";
            }
            else
            {
                mensaje = $"Te atacó {GameManager.Instance.EnemigoActual.Nombre}. Daño causado {resultadoAtaque.DanioAplicado}.";
            }
        }

        // informamos el evento
        EventosAcciones.Instancia.AgregarEventoInfo(mensaje);
    }

    public static void InformarEventoResultadoBatalla(ResultadoBatalla resultadoBatalla)
    {
        if (resultadoBatalla.Ganada)
        {
            // informamos el evento
            EventosAcciones.Instancia.AgregarEventoExito($"Ganaste {resultadoBatalla.PremioExperiencia} de exp. y {resultadoBatalla.PremioOro} de oro.");
        }
        else
        {
            // informamos el evento
            EventosAcciones.Instancia.AgregarEventoPeligro("Perdiste la batalla.");
        }
    }
}