using UnityEngine;

public class EnemigoMover : MonoBehaviour
{
	// variables p�blicas
	public float velocidad = 1;
	public float rango = 1;

	// variables privadas
	private Animator _animador;
	bool fuimosALaDerecha = true;
	Vector3 posicionOriginal;
	Vector3 posicionObjetivo;

	// Use this for initialization
	void Start()
	{
		_animador = gameObject.GetComponent<Animator>();
		posicionOriginal = transform.position;

		// calculamos la posici�n objetivo a la derecha
		posicionObjetivo = posicionOriginal + Vector3.right * rango;
	}

	// Update is called once per frame
	void Update()
	{
		// repetimos la acci�n de movernos
		Mover();
		// en el GAMEMANAGER tambi�n se rota GirarVistaEnemigoActual
		Reflejar();
	}

	void Mover()
	{
		// validamos si puede caminar
		if (!PuedeCaminar())
		{
			return;
		}

		// calculamos cuanto y a que velocidad se mover� el personaje		
		transform.position = Vector3.MoveTowards(transform.position, posicionObjetivo, velocidad * Time.deltaTime);

		// si la posici�n destino es la misma que la original damos la vuelta
		if (transform.position == posicionObjetivo)
		{
			fuimosALaDerecha = !fuimosALaDerecha;

			posicionObjetivo = fuimosALaDerecha ? posicionOriginal + Vector3.right * rango : posicionOriginal;
		}

		// actualizamos el valor Caminando en el animador para que lo escuchen las transiciones
		_animador.SetBool(AnimadorParametros.Caminando, true);
	}

	void Reflejar()
	{
		// validamos si puede caminar
		if (!PuedeCaminar())
		{
			return;
		}

		// si la tecla �ltima direcci�n fue la izquierda rotamos la animaci�n para que mire a ese lado
		if (!fuimosALaDerecha)
		{
			transform.localEulerAngles = new Vector3(0, 0, 0);
		}
		// si la tecla �ltima direcci�n fue la derecha rotamos la animaci�n para que mire a ese lado
		else
		{
			transform.localEulerAngles = new Vector3(0, 180, 0);
		}
	}

	private bool PuedeCaminar()
	{
		// si el enemigo est� muerto, o en batalla no puede hacer nada m�s
		if (
			!_animador.GetBool(AnimadorParametros.Vivo) ||
			_animador.GetBool(AnimadorParametros.EnBatalla))
		{
			// avisamos al animador que no est� caminando el enemigo
			_animador.SetBool(AnimadorParametros.Caminando, false);

			return false;
		}

		return true;
	}
}