using UnityEngine;

public class JugadorMover : MonoBehaviour
{
	// variables públicas
	public float velocidadMovimiento = 5;
	public float fuerzaSalto = 8;
	public float multiplicadorCaida = 0.5f;

	// variables privadas
	Rigidbody2D _jugador;
	Animator _animador;
	float vertical;
	float verticalAntesSalto;
	float horizontal;
	bool enElPiso;

	// Use this for initialization
	void Start ()
	{
		_jugador = GetComponent<Rigidbody2D>();
		_animador = GetComponent<Animator>();

		verticalAntesSalto = _animador.rootPosition.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// si el juego no inicio, no realizamos acción alguna
		if (!ValoresGlobales.JuegoIniciado)
		{
			return;
		}

		// si el jugador está muerto, en batalla, minando o talando no puede hacer nada más
		if (
			!_animador.GetBool(AnimadorParametros.Vivo) ||
			_animador.GetBool(AnimadorParametros.EnBatalla) ||
			_animador.GetBool(AnimadorParametros.Minando) ||
			_animador.GetBool(AnimadorParametros.Talando))
		{
			return;
		}

		// obtenemos los ejes para usar en cálculos
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		// ejecutamos las acciones de mover
		Mover();

		// ejecutamos las acciones de reflejar
		Reflejar();

		// ejecutamos las acciones de saltar
		Saltar();
	}

	//Detect collisions between the GameObjects with Colliders attached
	void OnCollisionEnter2D(Collision2D colision)
	{
		// verificamos si estamos tocando el piso
		if (colision.gameObject.CompareTag(Tags.Piso))
		{
			enElPiso = true;

			// avisamos al animador que aterrizamos
			_animador.SetBool(AnimadorParametros.Saltando, false);

			// guardamos la posición "y" antes de saltar para el reset de posición post batalla
			verticalAntesSalto = _animador.rootPosition.y;
		}
	}

	void OnCollisionExit2D(Collision2D colision)
	{
		// verificamos si estamos tocando el piso
		if (colision.gameObject.CompareTag(Tags.Piso))
		{
			// establecemos que dejamos de tocar el piso
			enElPiso = false;

			// guardamos la posición "y" antes de saltar para el reset de posición post batalla
			//verticalAntesSalto = _animador.rootPosition.y;

			// avisamos al animador que saltamos
			_animador.SetBool(AnimadorParametros.Saltando, true);
		}
	}

	void Mover()
	{
		// calculamos cuanto y a que velocidad se moverá el personaje
		_jugador.velocity = new Vector2(horizontal * velocidadMovimiento, _jugador.velocity.y);

		// actualizamos el valor Caminando en el animador para que lo escuchen las transiciones
		_animador.SetBool(AnimadorParametros.Caminando, horizontal != 0);

		// armamos la posición original con el eje "y" modificado para que si colisionamos mientras saltamos no aparezcamos en el aire post batalla
		Vector2 rootPosition = new Vector2(_animador.rootPosition.x, verticalAntesSalto);
		// actualizamos en el GameManager la posición actual del jugador y el origen de donde viene (derecha o izquierda)
		GameManager.Instance.EstablecerJugadorPosicionOriginal(rootPosition);
		GameManager.Instance.EstablecerJugadorPosicionHorizontalOrigen(horizontal);
	}

	void Reflejar()
	{
		// si la tecla presionada hace referencia a la izquierda rotamos la animación para que mire a ese lado
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.localEulerAngles = new Vector3(0, 180, 0);
		}
		// si la tecla presionada hace referencia a la derecha rotamos la animación para que mire a ese lado
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			transform.localEulerAngles = new Vector3(0, 0, 0);
		}
	}

	void Saltar()
	{
		// Si presiona la tecla arriba y estamos en el piso procedemos
		if (Input.GetKeyDown(KeyCode.UpArrow) && enElPiso)
		{
			// calculamos la altura a la que saltar y aplicamos
			_jugador.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
		}

		// si estamos cayendo aceleramos la caída
		if (_jugador.velocity.y < 0)
		{
			// calculamos a que velocidad caer y aplicamos
			_jugador.AddForce(Vector2.down * multiplicadorCaida, ForceMode2D.Impulse);
		}
	}
}