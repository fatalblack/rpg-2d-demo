using System.Collections;
using UnityEngine;

public static class JugadorColisionBatalla
{
	public static bool DetectarColisionBatalla(Collision2D colision)
	{
		bool esColision = false;

		// si colisionamos con un enemigo, inicia la batalla
		// nos basamos en el tag "enemigo"
		if (colision.gameObject.CompareTag(Tags.Enemigo))
		{
			// si estamos colisionando con un enemigo muerto no se tomará como batalla
			if (GameManager.Instance.EnemigoActual != null && GameManager.Instance.EnemigoActual.Estadistica.VidaActualCalculada < 1)
			{
				return esColision;
			}

			esColision = true;

			// precargamos los valores del enemigo para ser usado a posterior
			Animator animadorEnemigo = colision.gameObject.GetComponent<Animator>();
			Enemigo enemigo = CreadorEnemigos.CrearPorIdEnemigo(animadorEnemigo.GetInteger(AnimadorParametros.IdEnemigo), animadorEnemigo.GetInteger(AnimadorParametros.Nivel));
			
			GameManager.Instance.EstablecerAnimadorEnemigoActual(animadorEnemigo);
			GameManager.Instance.EstablecerEnemigoActual(enemigo);

			// giramos la vista del enemigo para estar enfrentados
			GameManager.Instance.GirarVistaEnemigoActual();
		}

		// retornamos el resultado de la colision
		return esColision;
	}

	public static void IniciarBatalla()
	{
		// ponemos el estado de "EnBatalla" del jugador y el enemigo en true
		GameManager.Instance.EstablecerInicioBatalla();

		// inicializamos los componentes para la batalla 
		ResultadoBatalla resultadoBatalla = BatallaMecanismos.Luchar(GameManager.Instance.EnemigoActual, true);

		// establecemos el resultado de la batalla
		GameManager.Instance.EstablecerResultadoUltimaBatalla(resultadoBatalla);
	}

	public static void FinalizarBatalla()
	{
		// inicializamos los componentes para la batalla 
		ResultadoBatalla resultadoBatalla = GameManager.Instance.ResultadoUltimaBatalla;

		// aplicamos los cambios de la batalla
		BatallaMecanismos.AplicarResultadoAlPersonaje(resultadoBatalla);

		// si ganamos la batalla debemos matar al enemigo
		if (resultadoBatalla.Ganada)
		{
			GameManager.Instance.MatarEnemigo();
		}
		// si perdemos la batalla significa que morimos, revivimos luego de esto
		else
		{
			GameManager.Instance.MatarYRevivirJugador();
		}

		// avisamos que finalizó la batalla
		GameManager.Instance.EstablecerFinBatalla();
	}

	public static IEnumerator AnimacionBatalla()
	{
		for (int i = 0; i < GameManager.Instance.ResultadoUltimaBatalla.DetalleAtaques.Count; i++)
		{
			ResultadoAtaque detalleAtaque = GameManager.Instance.ResultadoUltimaBatalla.DetalleAtaques[i];

			// cuando el jugador ataca
			if (detalleAtaque.EsTurnoJugador)
			{
				// actualizamos estado de ataque en el enemigo
				GameManager.Instance.ActualizarEstadoAtaqueAEnemigo(!detalleAtaque.Evadido);
			}
			// cuando el enemigo ataca
			else
			{
				// actualizamos estado de ataque en el jugador
				GameManager.Instance.ActualizarEstadoAtaqueAJugador(!detalleAtaque.Evadido);
			}

			// informamos el ataque realizado el visor de eventos
			BatallaMecanismos.InformarEventoAtaque(detalleAtaque);

			// demoramos un segundo entre cada detalle de Ataque para que se puedan ver las animaciones
			yield return new WaitForSeconds(1);

			// informamos el resultado de la batalla si es el último ataque
			if (i == GameManager.Instance.ResultadoUltimaBatalla.DetalleAtaques.Count - 1)
			{
				BatallaMecanismos.InformarEventoResultadoBatalla(GameManager.Instance.ResultadoUltimaBatalla);
			}
		}

		// quitamos el etado de atacantes
		GameManager.Instance.ReiniciarEstadoAtaqueAJugadorYEnemigo();

		// finalizamos la batalla
		FinalizarBatalla();
	}
}