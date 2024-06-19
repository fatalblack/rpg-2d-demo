#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class AccionPerderdor : MonoBehaviour
{
    // variables privadas
    Canvas _canvas;

	void Start()
	{
		_canvas = gameObject.GetComponentInChildren<Canvas>();
        _canvas.enabled = false;
    }

    void Update()
    {
        // si presiona la tecla Escape salimos del juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public static void Perdiste()
	{
        Canvas canvas = GameObject.FindGameObjectWithTag(Tags.CanvasPerdiste).GetComponentInChildren<Canvas>();

        // marcamos el juego como no perdido
        ValoresGlobales.JuegoPerdido = true;

        // marcamos el juego como no iniciado
        ValoresGlobales.JuegoIniciado = false;

        // mostramos el canvas de perdiste
        canvas.enabled = true;
    }

    public void Reiniciar()
    {
        // ocultamos el canvas
        _canvas.enabled = false;

        // marcamos el juego como no iniciado
        ValoresGlobales.JuegoIniciado = true;

        // marcamos el juego como no perdido
        ValoresGlobales.JuegoPerdido = false;
    }

    public void Salir()
	{
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}