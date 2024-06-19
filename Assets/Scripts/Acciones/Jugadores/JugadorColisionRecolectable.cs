using UnityEngine;

public static class JugadorColisionRecolectable
{
	public static bool DetectarColisionRecolectable(Collider2D colision)
	{
		bool esColision = false;
		Recolectable recolectable = null;

		// si colisionamos con un recolectable, nos habilita el poder recolectar
		// nos basamos en el tag "ArbolRoble"
		if (colision.gameObject.CompareTag(Tags.ArbolRoble))
		{
			esColision = true;

			// precargamos los valores del recolectable para ser usado a posterior
			recolectable = CreadorRecolectables.CrearArbolRoble();
		}
		// nos basamos en el tag "ArbolRoble"
		if (colision.gameObject.CompareTag(Tags.MenaBronce))
		{
			esColision = true;

			// precargamos los valores del recolectable para ser usado a posterior
			recolectable = CreadorRecolectables.CrearMenaBronce();
		}

		// si hay colisión recién establecemos el animador
		if (esColision)
		{
			// precargamos los valores del recolectable para ser usado a posterior
			Animator animadorRecolectable = colision.gameObject.GetComponent<Animator>();

			// establecemos el recolectable actual en el GameManager
			GameManager.Instance.EstablecerAnimadorRecolectableActual(animadorRecolectable);
			GameManager.Instance.EstablecerRecolectableActual(recolectable);
		}

		// retornamos el resultado de la colision
		return esColision;
	}
}