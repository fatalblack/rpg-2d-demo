using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletarBienvenida : MonoBehaviour
{
    // variables públicas
    public TMP_InputField InputNombrePersonaje;

    private void Start()
    {
        if (!Application.isEditor)
        {
            SceneManager.LoadScene(Escenas.EscenaJuego, LoadSceneMode.Additive);
        }
    }

    public void IrAlJuego()
    {
        // establecemos el nombre del personaje
        GameManager.Instance.Jugador.Nombre = InputNombrePersonaje.text ?? "Anonimous XD";

        // establecemos que el juego acaba de iniciar
        ValoresGlobales.JuegoIniciado = true;

        // cambiamos de escena
        SceneManager.UnloadSceneAsync(Escenas.EscenaBienvenida);
    }
}