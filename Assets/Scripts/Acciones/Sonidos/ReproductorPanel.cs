using UnityEngine;

public class ReproductorPanel : MonoBehaviour
{
	// variables públicas
	public AudioSource audioSource;
	public AudioClip audioAbrirVentana;
	public AudioClip audioCerrarVentana;
	public AudioClip audioEquiparItem;
	public AudioClip audioUsarItem;
	public AudioClip audioVenderComprar;
	public AudioClip audioError;

	public void ReproducirAbrirVentana()
	{
		Reproducir(audioAbrirVentana);
	}

	public void ReproducirCerrarVentana()
	{
		Reproducir(audioCerrarVentana);
	}

	public void ReproducirEquiparItem()
	{
		Reproducir(audioEquiparItem);
	}

	public void ReproducirUsarItem()
	{
		Reproducir(audioUsarItem);
	}

	public void ReproducirVenderComprar()
	{
		Reproducir(audioVenderComprar);
	}

	public void ReproducirError()
	{
		Reproducir(audioError);
	}

	private void Reproducir(AudioClip audioClip)
	{
		audioSource.clip = audioClip;
		audioSource.Play();
	}
}