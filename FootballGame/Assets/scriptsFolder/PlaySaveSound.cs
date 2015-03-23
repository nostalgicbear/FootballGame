using UnityEngine;
using System.Collections;

/*
 * THis is attached to the goalkeeper.
 */ 

public class PlaySaveSound : MonoBehaviour {

	public AudioClip saveSound;
	private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
	}

	/*
	 * When the ball collides with the goalkeeper, he has made a save, and so I play a save sound
	 */
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			audioSource.audio.PlayOneShot(saveSound, 1.0f);
		}
	}
}
