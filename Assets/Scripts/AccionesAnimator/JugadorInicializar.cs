using UnityEngine;

public class JugadorInicializar : MonoBehaviour
{
    // variables privadas
    Animator _animador;

    // Start is called before the first frame update
    void Start()
    {
        _animador = GetComponent<Animator>();

		// establecemos el animator en el GameManager
		GameManager.Instance.EstablecerAnimadorJugador(_animador);

		// establecemos si está vivo en el animador en base al personaje del GameManager
		_animador.SetBool(AnimadorParametros.Vivo, true);
	}

	//Detect collisions between the GameObjects with Colliders attached
	void OnCollisionEnter2D(Collision2D colision)
	{
		EvaluarColisionBatalla(colision);
	}

	private void OnTriggerEnter2D(Collider2D colision)
	{
		EvaluarColisionRecolectable(colision);
	}

	private void EvaluarColisionBatalla(Collision2D colision)
	{
		// verificamos si es colisión de batalla
		bool esColisionBatalla = JugadorColisionBatalla.DetectarColisionBatalla(colision);

		// si es colisión procedemos con los pasos para batallar
		if (esColisionBatalla)
		{
			// iniciamos la batalla
			JugadorColisionBatalla.IniciarBatalla();

			// mostramos la animación de la batalla
			StartCoroutine(JugadorColisionBatalla.AnimacionBatalla());
		}
	}

	private void EvaluarColisionRecolectable(Collider2D colision)
	{
		JugadorColisionRecolectable.DetectarColisionRecolectable(colision);
	}
}