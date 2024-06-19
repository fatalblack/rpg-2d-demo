using System;
using UnityEngine;

public class SpawnerEnemigo : MonoBehaviour
{
    // variables públicas
    //public GameObject enemigo;
    public int spawnearDespuesDeXSegundos;

    // variables privadas
    Animator _animador;
    bool spawneando = false;

	void Start()
	{
        _animador = gameObject.GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update()
    {
        bool enemigoVivo = _animador.GetBool(AnimadorParametros.Vivo);

        // si no hay un enemigo y no estamos en proceso de spawnear uno, procedemos
        //if (_enemigoGenerado == null && !spawneando)
        if (!enemigoVivo && !spawneando)
        {
            // establecemos en el proceso que estamos spawneando
            spawneando = true;

            // instanciamos el enemigo
            StartCoroutine(InstanciarEnemigo());
        }
    }

    private System.Collections.IEnumerator InstanciarEnemigo()
    {
        // iniciamos el respawn en X segundos (spawnearDespuesDeXSegundos)
        //yield return new WaitForSeconds(spawnearDespuesDeXSegundos);
        GameObject enemigoRespawn = GameObject.Instantiate(gameObject);
        
        // ponemos el enemigo desactivado para que de tiempo de que la animación de muerte se ejecute
        enemigoRespawn.SetActive(false);
        
        // asignamos el padre del enemigo muerto a este enemigo nuevo
        Transform transformPadre = transform.parent.transform;
        enemigoRespawn.transform.SetParent(transformPadre);
        
        // le ponemos un nombre random al objeto porque sino empieza a desbordar concatenando la palabra (clone)
        enemigoRespawn.name = Guid.NewGuid().ToString();
        
        // ocutalmos el enemigo muerto pasados 2 segundo
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // volvemos a activar el nuevo enemigo en X segundos (spawnearDespuesDeXSegundos)
        yield return new WaitForSeconds(spawnearDespuesDeXSegundos);
        
        // ponemos el enemigo desactivado para que de tiempo de que la animación de muerte se ejecute
        enemigoRespawn.SetActive(true);
        
        // destruimos el enemigo muerto pasados 1 segundos
        yield return new WaitForSeconds(1);
        GameObject.Destroy(gameObject);
    }
}