using System.Linq;
using TMPro;
using UnityEngine;

public class EnemigoInicializar : MonoBehaviour
{
	// variables públicas
	public int nivelBaseMaximo;
	public EnemigoIndices indiceEnemigo;

	// variables privadas
	Animator _animador;
	Canvas _ventanaEnemigo;
	int nivelBase;
	private Enemigo _enemigo;

	// Use this for initialization
	void Start()
	{
		_animador = GetComponent<Animator>();
		_ventanaEnemigo = GameObject.FindGameObjectWithTag(Tags.VentanaEnemigo).gameObject.GetComponent<Canvas>();

		// calculamos el nivelBase aleatorio
		nivelBase = ObtenerNivelBaseAleatorio();

		// si no se creo el esqueleto lo creamos (primera vez)
		if (_enemigo == null)
		{
			_enemigo = CreadorEnemigos.CrearPorIdEnemigo((int)indiceEnemigo, nivelBase);

			// actualizamos las variables necesarias en el animador
			_animador.SetBool(AnimadorParametros.Vivo, _enemigo.Estadistica.VidaActualCalculada > 0);
			_animador.SetInteger(AnimadorParametros.Nivel, _enemigo.Nivel);
			_animador.SetInteger(AnimadorParametros.IdEnemigo, _enemigo.Id);

			// actualizamos el título del enemigo
			ActualizarTituloEnemigo();
		}
	}

	// Update is called once per frame
	void Update()
	{
		// la formula para calcular el nivel es (nivelBase - 1 + nivel jugador)
		int nuevoNivel = nivelBase - 1 + GameManager.Instance.Jugador.Nivel;

		// si el nivel del jugador es superior a 1 y el nivel del enemigo no se actualizó, debemos actualizarlo
		if (GameManager.Instance.Jugador.Nivel > 1 && nuevoNivel > _enemigo.Nivel)
		{
			// actualizamos el nivel del enemigo
			_enemigo = CreadorEnemigos.CrearPorIdEnemigo((int)indiceEnemigo, nuevoNivel);
			_animador.SetInteger(AnimadorParametros.Nivel, nuevoNivel);
		}
	}

	// al posicionar el puntero del mouse sobre el enemigo mostramos la información del mismo como un tooltip
	void OnMouseOver()
	{
		// en el tooltip modificamos el título
		TextMeshProUGUI titulo = _ventanaEnemigo.GetComponentsInChildren<TextMeshProUGUI>().First(texto => texto.CompareTag(Tags.Titulo));
		titulo.text = $"{_enemigo.Nombre} (Nv. {_enemigo.Nivel})";
		EnemigoEstadistica estadistica = _enemigo.Estadistica;

		// en el tooltip modificamos la descripción
		TextMeshProUGUI descripcion = _ventanaEnemigo.GetComponentsInChildren<TextMeshProUGUI>().First(texto => texto.CompareTag(Tags.Descripcion));
		descripcion.text = $"<color=\"white\">FUE:</color> {estadistica.Fuerza} <pos=50%><color=\"white\">ATQ:</color> {estadistica.AtaqueCalculado}\r\n";
		descripcion.text += $"<color=\"white\">AGI:</color> {estadistica.Agilidad} <pos=50%><color=\"white\">V.ATQ:</color> {estadistica.VelocidadAtaqueCalculada}\r\n";
		descripcion.text += $"<color=\"white\">INT:</color> {estadistica.Inteligencia} <pos=50%><color=\"white\">EVA:</color> {estadistica.EvasionCalculada}\r\n";
		descripcion.text += $"<color=\"white\">VIT:</color> {estadistica.Vitalidad} <pos=50%><color=\"white\">VIDA:</color> {estadistica.VidaMaximaCalculada}\r\n";

		// al posicionar el puntero del mouse sobre el enemigo mostramos la información del mismo como un tooltip
		_ventanaEnemigo.enabled = true;
	}

	void OnMouseExit()
	{
		// al sacar el puntero del mouse del enemigo ocultamos el tooltip
		_ventanaEnemigo.enabled = false;
	}

	// actualizamos el título del enemigo
	private void ActualizarTituloEnemigo()
	{
		/*TextMeshPro titulo = _animador.gameObject.GetComponentsInChildren<TextMeshPro>().First(texto => texto.CompareTag(Tags.Titulo));
		titulo.text = $"{_enemigo.Nombre} (Nv. {nivel})";*/
	}

	private int ObtenerNivelBaseAleatorio()
	{
		return Random.Range(1, nivelBaseMaximo + 1);
	}
}