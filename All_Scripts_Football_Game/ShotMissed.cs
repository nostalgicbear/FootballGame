using UnityEngine;
using System.Collections;

/*
 * This is placed on triggers that are placed either side of the goal, and over the top of the goal. They are triggered
 * when a player misses the goal.
 */ 

public class ShotMissed : MonoBehaviour {
	public AudioClip missSound;

	/*
	 * When the ball collides with the objects these triggers are attached to, the player has missed and so I play a 
	 * sound that indicates the player has missed
	 */ 
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			audio.PlayOneShot(missSound, 0.7f);
		}
	}
}
