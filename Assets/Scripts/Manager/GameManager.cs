using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// parametros desde públicos
	public int IdPersonaje = 1;
	public int InventarioMaximo = 35;
	public int ApilableMaximo = 9999;
	public ReproductorPanel reproductorPanel;

	// variables privadas
	private string nombrePersonaje;

	// instancia de GameManager para singleton
	public static GameManager Instance { get; private set; }
    
	// parametros relevantes para recoger data en el juego
    public Personaje Jugador { get { return _jugador; } }

    private Personaje _jugador;

	public Animator AnimadorJugador { get { return _animadorJugador; } }

	private Animator _animadorJugador;

	public Enemigo EnemigoActual { get { return _enemigoActual; } }

	private Enemigo _enemigoActual;

	private Enemigo _enemigoMuerto;

	public Animator AnimadorEnemigoActual { get { return _animadorEnemigoActual; } }

	private Animator _animadorEnemigoActual;

	private Animator _animadorEnemigoMuerto;

	public ResultadoBatalla ResultadoUltimaBatalla { get { return _resultadoUltimaBatalla; } }

	private ResultadoBatalla _resultadoUltimaBatalla;

	public Recolectable RecolectableActual { get { return _recolectableActual; } }

	private Recolectable _recolectableActual;

	public Animator AnimadorRecolectableActual { get { return _animadorRecolectableActual; } }

	private Animator _animadorRecolectableActual;

	public List<Objeto> Tienda { get { return _tienda; } }

	private List<Objeto> _tienda;

	private bool InventarioSincronizado { get; set; }

	public int CantidadMaximaEspaciosInventario { get { return InventarioMaximo; } }

	public int CantidadMaximaObjetosApilables { get { return ApilableMaximo; } }

	public bool PuedeVender { get { return _puedeVender; } }

	private bool _puedeVender;

	private Vector3 _jugadorPosicionInicioJuego;

	private Vector3 _jugadorPosicionOriginal;

	private float _jugadorPosicionHorizontalOrigen;

	private Vector3? _enemigoPosicionOriginal;

	private GameObject _escenarioBatalla;

	// inicializamos el singleton en caso de no estarlo
	private void Awake()
	{
		if (Instance == null)
		{
			// creamos el personaje para el jugador
			_jugador = CreadorPersonajes.Crear(IdPersonaje, nombrePersonaje);

			// establecemos la posición donde inició el jugador, para respawn en caso de morir
			_jugadorPosicionInicioJuego = GameObject.FindGameObjectWithTag(Tags.Jugador).transform.position;

			// creamos el enemigo en nulo inicialmente
			_enemigoActual = null;

			// renovamos stock de la tienda
			_tienda = TiendaAcciones.ObtenerStockTienda();

			// obtenemos el GameObject del escenario de batalla
			EstablecerEscenarioBatalla();

			// dejamos la instancia como el estado actual del GameManager
			Instance = this;

			// le ponemos por defecto 2 pociones menores de vida
			PersonajeInventarioAcciones.AgregarObjetoInventario(CreadorObjetos.CrearPocionVidaMenor(), 2);
		}
	}

	public void EstablecerAnimadorJugador(Animator animator)
	{
		_animadorJugador = animator;
		_jugadorPosicionOriginal = animator.transform.position;
	}

	public void EstablecerEnemigoActual(Enemigo enemigo)
	{
		_enemigoActual = enemigo;
	}

	public void EstablecerAnimadorEnemigoActual(Animator animator)
	{
		_animadorEnemigoActual = animator;
		_enemigoPosicionOriginal = new Vector3(animator.transform.position.x, animator.transform.position.y);
	}

	public void EstablecerInicioBatalla()
	{
		// actualizamos la posición actual del enemigo
		EstablecerEnemigoPosicionOriginal(_animadorEnemigoActual.transform.position);

		// mostramos el escenario de batalla
		MostrarEscenarioBatalla();

		// ponemos el estado de "EnBatalla" del jugador y el enemigo en true
		_animadorJugador.SetBool(AnimadorParametros.EnBatalla, true);
		_animadorJugador.SetBool(AnimadorParametros.Caminando, false);
		_animadorEnemigoActual.SetBool(AnimadorParametros.EnBatalla, true);
	}

	public void EstablecerResultadoUltimaBatalla(ResultadoBatalla resultadoBatalla)
	{
		_resultadoUltimaBatalla = resultadoBatalla;
	}

	public void EstablecerFinBatalla()
	{
		// ocultamos el escenario de batalla
		OcultarEscenarioBatalla();

		// movemos el jugador a la posición indicada
		MoverJugadorFinBatalla();

		// avisamos que finalizó la batalla
		_animadorJugador.SetBool(AnimadorParametros.EnBatalla, false);
		if (_animadorEnemigoActual != null)
		{
			_animadorEnemigoActual.SetBool(AnimadorParametros.EnBatalla, false);
		}
	}

	public void EstablecerRecolectableActual(Recolectable recolectable)
	{
		_recolectableActual = recolectable;
	}

	public void EstablecerAnimadorRecolectableActual(Animator animator)
	{
		_animadorRecolectableActual = animator;
	}

	public void MatarYRevivirJugador()
	{
		// mostramos el canvas de perdiste
		AccionPerderdor.Perdiste();

		// seteamos los datos del jugador indicando que murió
		_animadorJugador.SetBool(AnimadorParametros.EnBatalla, false);
		_animadorJugador.SetBool(AnimadorParametros.Vivo, false);

		// revivimos al jugador
		StartCoroutine(RevivirJugador());
	}

	private System.Collections.IEnumerator RevivirJugador()
	{
		// si el jugador está muerto procedemos
		if (!_animadorJugador.GetBool(AnimadorParametros.Vivo))
		{
			// agregamos un delay antes de revivir cosa que se vea el cadaver xD
			yield return new WaitForSeconds(2);

			// restauramos la vida del jugador
			PersonajeAcciones.CurarVida(_jugador.Estadistica.VidaMaximaCalculada);
			_animadorJugador.SetBool(AnimadorParametros.Vivo, true);

			// hacemos respawn del jugador en el punto donde inició el juego
			_animadorJugador.transform.position = _jugadorPosicionInicioJuego;
		}
	}

	public void MatarEnemigo()
	{
		// seteamos el enemigo muerto como historial y para revivirlo de ser necesario
		_animadorEnemigoMuerto = _animadorEnemigoActual;
		_enemigoMuerto = _enemigoActual;

		// al haber muerto el enemigo liberamos toda animación de él que indicaba batalla y vida
		_animadorEnemigoActual.SetBool(AnimadorParametros.EnBatalla, false);
		_animadorEnemigoActual.SetBool(AnimadorParametros.Vivo, false);

		// desasignamos el enemigo del manager
		_animadorEnemigoActual = null;		
		_enemigoActual = null;
	}

	public System.Collections.IEnumerator RevivirEnemigo()
	{
		// si tenemos un enemigo muerto procedemos
		if (_enemigoMuerto != null)
		{
			// agregamos un delay antes de revivir cosa que se vea el cadaver xD
			yield return new WaitForSeconds(2);

			// reasignamos el enemigo muerto como enemigo actual
			_enemigoActual = _enemigoMuerto;
			_animadorEnemigoActual = _animadorEnemigoMuerto;

			// restauramos la vida del enemigo
			EnemigoActual.Estadistica.VidaActualCalculada = EnemigoActual.Estadistica.VidaMaximaCalculada;
			_animadorEnemigoActual.SetBool(AnimadorParametros.Vivo, EnemigoActual.Estadistica.VidaActualCalculada > 0);
		}
	}

	public void ActualizarEstadoAtaqueAEnemigo(bool ataqueExitoso)
	{
		// actualizamos estado de ataque en el enemigo
		_animadorEnemigoActual.SetBool(AnimadorParametros.AdvertirAtaqueEnemigo, true);
		_animadorEnemigoActual.SetBool(AnimadorParametros.Golpeado, ataqueExitoso);
		_animadorEnemigoActual.SetBool(AnimadorParametros.Atacando, false);

		// informamos que el jugador no está siendo atacado
		_animadorJugador.SetBool(AnimadorParametros.AdvertirAtaqueEnemigo, false);
		_animadorJugador.SetBool(AnimadorParametros.Golpeado, false);
		_animadorJugador.SetBool(AnimadorParametros.Atacando, true);
	}

	public void ActualizarEstadoAtaqueAJugador(bool ataqueExitoso)
	{
		// actualizamos estado de ataque en el jugador
		_animadorJugador.SetBool(AnimadorParametros.AdvertirAtaqueEnemigo, true);
		_animadorJugador.SetBool(AnimadorParametros.Golpeado, ataqueExitoso);
		_animadorJugador.SetBool(AnimadorParametros.Atacando, false);

		// informamos que el enemigo no está siendo atacado
		_animadorEnemigoActual.SetBool(AnimadorParametros.AdvertirAtaqueEnemigo, false);
		_animadorEnemigoActual.SetBool(AnimadorParametros.Golpeado, false);
		_animadorEnemigoActual.SetBool(AnimadorParametros.Atacando, true);
	}

	public void ReiniciarEstadoAtaqueAJugadorYEnemigo()
	{
		// quitamos el etado de atacante del jugador
		_animadorJugador.SetBool(AnimadorParametros.AdvertirAtaqueEnemigo, false);
		_animadorJugador.SetBool(AnimadorParametros.Golpeado, false);
		_animadorJugador.SetBool(AnimadorParametros.Atacando, false);

		// quitamos el etado de atacante del enemigo
		_animadorEnemigoActual.SetBool(AnimadorParametros.AdvertirAtaqueEnemigo, false);
		_animadorEnemigoActual.SetBool(AnimadorParametros.Golpeado, false);
		_animadorEnemigoActual.SetBool(AnimadorParametros.Atacando, false);
	}

	public void InventarioModificado()
	{
		InventarioSincronizado = false;
	}

	public bool SeDebeSincronizarInventario()
	{
		return !InventarioSincronizado;
	}

	public void SeSincronizoInventario()
	{
		InventarioSincronizado = true;
	}

	public void EstablecerPuedeVender(bool puedeVender)
	{
		_puedeVender = puedeVender;
	}

	public void EstablecerJugadorPosicionOriginal(Vector3 posicionActual)
	{
		_jugadorPosicionOriginal = posicionActual;
	}

	public void EstablecerJugadorPosicionHorizontalOrigen(float posicionOrigen)
	{
		_jugadorPosicionHorizontalOrigen = posicionOrigen;
	}

	public void EstablecerEnemigoPosicionOriginal(Vector3 posicionActual)
	{
		_enemigoPosicionOriginal = posicionActual;
	}

	// giramos la vista del enemigo para que nos mire en batalla
	public void GirarVistaEnemigoActual()
	{
		Transform enemigoTransform = _animadorEnemigoActual.transform;
		// acomodamos el ángulo de visión del enemigo de acuerdo a nuestro ángulo de visión
		enemigoTransform.rotation = _animadorJugador.transform.rotation;
	}

	private void EstablecerEscenarioBatalla()
	{
		// obtenemos el GameObject del escenario de batalla
		_escenarioBatalla = GameObject.FindGameObjectWithTag(Tags.EscenarioBatalla);

		// ocultamos por defecto el escenario
		OcultarEscenarioBatalla();
	}

	private void MostrarEscenarioBatalla()
	{
		// mostramos el escenario de batalla
		_escenarioBatalla.SetActive(true);
	}

	private void OcultarEscenarioBatalla()
	{
		// ocultamos el escenario de batalla
		_escenarioBatalla.SetActive(false);
	}

	private void MoverJugadorFinBatalla()
	{
		// calculamos desde donde vino la ultima vez el jugador
		// si horizontal es negativo significa que se movía hacia la izquierda, caso contrario hacia la derecha
		float modificadorHorizontal = _jugadorPosicionHorizontalOrigen >= 0 ? -1 : 1;
		// movemos al jugador a su posición original pero lo movemos un poquito al costado para que no se provoque un loop infinito de colisión con el enemigo
		_animadorJugador.transform.position = new Vector3(_jugadorPosicionOriginal.x + modificadorHorizontal, _jugadorPosicionOriginal.y);

		// si el enemigo murió debemos mover el cuerpo xD
		if (_animadorEnemigoActual == null)
		{
			// respawneamos el enemigo con más delay que el destruir cosa que aparezca en el mismo lugar
			//StartCoroutine(ReSpawnearEnemigo());
		}
		else
		{
			// si el enemigo está vivo lo retornamos a su posición original
			_animadorEnemigoActual.transform.position = _enemigoPosicionOriginal.Value;
		}
	}

	/*private System.Collections.IEnumerator ReSpawnearEnemigo()
	{
		// iniciamos el respawn en 5 segundos
		yield return new WaitForSeconds(5);
		GameObject enemigoRespawn = GameObject.Instantiate(_animadorEnemigoMuerto.gameObject);
		// ponemos el enemigo desactivado para que de tiempo de que la animación de muerte se ejecute
		enemigoRespawn.SetActive(false);
		// asignamos el padre del enemigo muerto a este enemigo nuevo
		Transform transformPadre = _animadorEnemigoMuerto.transform.parent.transform;
		enemigoRespawn.transform.SetParent(transformPadre);
		// le ponemos un nombre random al objeto porque sino empieza a desbordar concatenando la palabra (clone)
		enemigoRespawn.name = Guid.NewGuid().ToString();
		// destruimos el enemigo muerto
		GameObject.Destroy(_animadorEnemigoMuerto.gameObject);
		// volvemos a activar el nuevo enemigo en 5 segundos
		yield return new WaitForSeconds(5);
		// ponemos el enemigo desactivado para que de tiempo de que la animación de muerte se ejecute
		enemigoRespawn.SetActive(true);
	}*/
}