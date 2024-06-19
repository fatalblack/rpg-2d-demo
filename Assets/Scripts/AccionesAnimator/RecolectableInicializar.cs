using System.Linq;
using UnityEngine;

public class RecolectableInicializar : MonoBehaviour
{
	// variables privadas
	Animator _alertaAnimador;

    // Start is called before the first frame update
    void Start()
    {
		_alertaAnimador = GetComponentsInChildren<Animator>().First(animator => animator.CompareTag(Tags.Alerta));
    }

	//Detect collisions between the GameObjects with Colliders attached
	private void OnTriggerEnter2D()
	{
		// al haber colisión debemos activar la alerta
		_alertaAnimador.SetBool(AnimadorParametros.EstaColisionado, true);
	}

	private void OnTriggerExit2D()
	{
		// al no haber colisión debemos desactivar la alerta
		_alertaAnimador.SetBool(AnimadorParametros.EstaColisionado, false);

		// vaciamos el recolectable actual del GameManager
		GameManager.Instance.EstablecerAnimadorRecolectableActual(null);
		GameManager.Instance.EstablecerRecolectableActual(null);
	}
}