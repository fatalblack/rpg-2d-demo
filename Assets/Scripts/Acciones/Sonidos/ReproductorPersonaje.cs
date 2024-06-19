using UnityEngine;

public class ReproductorPersonaje : MonoBehaviour
{
	// variables públicas
	public AudioSource audioSource;
	public AudioClip audioAtacar;
	public AudioClip audioGolpeado;
	public AudioClip audioCaminarPasto;
	public AudioClip audioCaminarRoca;
	public AudioClip audioSaltar;
	public AudioClip audioMorir;
	public AudioClip audioTalar;
	public AudioClip audioMinar;

	public void ReproducirAtacar()
	{
		Reproducir(audioAtacar);
	}

	public void ReproducirGolpeado()
	{
		Reproducir(audioGolpeado);
	}

	public void ReproducirCaminarPasto()
	{
		Reproducir(audioCaminarPasto);
	}

	public void ReproducirCaminarRoca()
	{
		Reproducir(audioCaminarRoca);
	}

	public void ReproducirSaltar()
	{
		Reproducir(audioSaltar);
	}

	public void ReproducirMorir()
	{
		Reproducir(audioMorir);
	}

	public void ReproducirTalar()
	{
		Reproducir(audioTalar);
	}

	public void ReproducirMinar()
	{
		Reproducir(audioMinar);
	}

	private void Reproducir(AudioClip audioClip)
	{
		audioSource.clip = audioClip;
		audioSource.Play();
	}
}