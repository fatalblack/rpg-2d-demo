using System.Collections;
using UnityEngine;

public class JugadorOficioAcciones : MonoBehaviour
{
    // componentes de GameManager
    private static Personaje _personaje;
    private static Recolectable _recolectableActual;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _personaje ??= GameManager.Instance.Jugador;
        _recolectableActual = GameManager.Instance.RecolectableActual;

        // si presiona la E debemos verificar si puede recolectar
        if (Input.GetKeyDown(KeyCode.E))
        {
			//si puede ejercer el oficio procedemos, sino no hacemos nada
			if (PuedeEjercerOficio())
			{
                Recolectar();
                StartCoroutine(DetenerRecolectar());
            }
        }
    }

    //private IEnumerator Recolectar()
    private void Recolectar()
    {
        if (_recolectableActual != null)
		{
            // establecemos que acción se está realizando
            string accion = ObtenerAccion();

            // establecemos en el animator del personaje la acción realizada
            GameManager.Instance.AnimadorJugador.SetBool(accion, true);

            // recolectamos el material que corresponda
            OficioAcciones.ExtraerDeRecolectable(_recolectableActual);
        }
	}
    private IEnumerator DetenerRecolectar()
    {
        // limitamos a que cada recolección se haga cada 0.5 segundos
        yield return new WaitForSeconds(0.5f);
        // recolectamos
        string accion = ObtenerAccion();
        // avisamos al animador
        GameManager.Instance.AnimadorJugador.SetBool(accion, false);        
    }

    private string ObtenerAccion()
	{
        string accion = string.Empty;

        if (_recolectableActual != null)
		{
            // establecemos que acción se está realizando
            if (_recolectableActual.Caracteristica.SePuedeTalar)
            {
                accion = AnimadorParametros.Talando;
            }
            if (_recolectableActual.Caracteristica.SePuedeMinar)
            {
                accion = AnimadorParametros.Minando;
            }
        }

        return accion;    
    }

    private static bool PuedeEjercerOficio()
    {
        bool tieneHerramientaTala = _personaje.Inventario.Exists(item => item.Objeto.Caracteristica.ParaTalar);
        bool tieneHerramientaMineria = _personaje.Inventario.Exists(item => item.Objeto.Caracteristica.ParaMinar);

        // si no hay un recolectable actual retornamos falso
        if (_recolectableActual == null)
        {
            // no puede ejercer la tala
            return false;
        }

        // si se debe talar pero no contamos con la herramienta necesaria no continuamos
        if (_recolectableActual.Caracteristica.SePuedeTalar && !tieneHerramientaTala)
        {
            // no puede ejercer la tala
            return false;
        }

        // si se debe minar pero no contamos con la herramienta necesaria no continuamos
        if (_recolectableActual.Caracteristica.SePuedeMinar && !tieneHerramientaMineria)
        {
            // no puede ejercer la mineria
            return false;
        }

        // puede ejercer el oficio
        return true;
    }
}