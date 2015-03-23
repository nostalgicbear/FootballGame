using UnityEngine;
using System.Collections;

public class FansCelebrate : MonoBehaviour {

	public GameObject fan1;
	public FanScript fan;
	public AudioClip audioClip;

	private GameObject[] fans;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		fans = GameObject.FindGameObjectsWithTag("Fan"); //I find all the fans and add them to an array
		audioSource = GetComponent<AudioSource>();
	}
	
	/*
	 *If the ball enters the goal, I play a sound of fans celebrating. I then cycle through every fan in the scene
	 *and change their state from RESTING to CELEBRATING. This is to make it look natural when the player scores.
	 */
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			audioSource.audio.PlayOneShot(audioClip, 1.0f);
			fan.state = FanScript.Fan_State.CELEBRATING;
			foreach(GameObject f in fans) //This foreach loop changes the fans states to make them celebrate
			{
				f.gameObject.animation.Play("celebrate");
				f.GetComponent<FanScript>().state = FanScript.Fan_State.CELEBRATING;
				fan.state = FanScript.Fan_State.CELEBRATING;
			}
		}
	}
}
