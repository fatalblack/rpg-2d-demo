using UnityEngine;
using UnityEngine.UI;

public class BotonManejarAudioSource : MonoBehaviour
{
    // variables públicas
    public AudioSource audioSource;
    public bool muteado;
    public Sprite imagenActivo;
    public Sprite imagenInactivo;

    // variables privadas
    Image _imagenBoton;

    private void Start()
    {
        _imagenBoton = gameObject.GetComponent<Image>();
    }

    public void AlternarMuteo()
    {
        // alternamos el muteo, si estaba muteado ahora ya no y viceversa
        muteado = !muteado;

        // dependiendo si se muteó o no, tomarémos esa acción en el audioSource
        audioSource.mute = muteado;

        // cambiamos la imagen del botón de acuerdo a la elección
        _imagenBoton.sprite = muteado ? imagenInactivo : imagenActivo;
    }
}