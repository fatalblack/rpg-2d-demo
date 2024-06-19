using UnityEngine;

public class ReproductorEsqueleto : MonoBehaviour
{
	// variables públicas
	public AudioClip audioAtacar;
	public AudioClip audioGolpeado;
	public AudioClip audioCaminarPasto;
	public AudioClip audioCaminarRoca;
	public AudioClip audioMorir;

	// variables privadas
	AudioSource _audioSource;

	// Use this for initialization
	void Start()
	{
		_audioSource = GameObject.FindGameObjectWithTag(Tags.AudioSource).GetComponent<AudioSource>();
	}

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
		//Reproducir(audioCaminarPasto);
	}

	public void ReproducirCaminarRoca()
	{
		Reproducir(audioCaminarRoca);
	}

	public void ReproducirMorir()
	{
		Reproducir(audioMorir);
	}

	private void Reproducir(AudioClip audioClip)
	{
		_audioSource.clip = audioClip;
		_audioSource.Play();
	}
}