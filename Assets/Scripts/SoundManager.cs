using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip[] list;
    AudioClip currentAudio;
    // Use this for initialization
    void Start () {
 
    }
	
	// Update is called once per frame
	public void PlayLightHit()
    {
        currentAudio = list[0];
        GetComponent<AudioSource>().PlayOneShot(currentAudio);
    }

    public void PlayMediumHit()
    {
        currentAudio = list[1];
        GetComponent<AudioSource>().PlayOneShot(currentAudio);
    }

    public void PlayEnderHit()
    {
        currentAudio = list[2];
        GetComponent<AudioSource>().PlayOneShot(currentAudio);
    }

    public void PlayEnderHit2()
    {
        currentAudio = list[3];
        GetComponent<AudioSource>().PlayOneShot(currentAudio);
    }
    public void PlayLowWhoosh()
    {
        currentAudio = list[5];
        GetComponent<AudioSource>().PlayOneShot(currentAudio);
    }

    public void PlayHighWhoosh()
    {
        currentAudio = list[6];
        GetComponent<AudioSource>().PlayOneShot(currentAudio);
    }

    public void PlayPlayerHurt()
	{
		currentAudio = list[7];
		GetComponent<AudioSource>().PlayOneShot(currentAudio);
	}

	public void PlayExplosion()
	{
		currentAudio = list[8];
		GetComponent<AudioSource>().PlayOneShot(currentAudio);
	}
    public void PlayEarthQuake()
    {
        currentAudio = list[9];
        GetComponent<AudioSource>().PlayOneShot(currentAudio);
    }
    public void PlayEndMusic()
    {
        currentAudio = list[10];
        GetComponent<AudioSource>().PlayOneShot(currentAudio);
    }

	public void PlaySiren()
	{
		currentAudio = list[11];
		GetComponent<AudioSource>().PlayOneShot(currentAudio);
	}
}
