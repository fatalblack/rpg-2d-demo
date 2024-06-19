using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    // variables públicas
    public GameObject jugador;
    public GameObject cielo;
    public GameObject fondoBatalla;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // movemos la cámara para que siga al jugador
        transform.position = jugador.transform.position;// + distancia;

        // movemos al cielo para que se acomode a la cámara
        cielo.transform.position = new Vector3(jugador.transform.position.x, jugador.transform.position.y + 5.5f, jugador.transform.position.z);
    }
}