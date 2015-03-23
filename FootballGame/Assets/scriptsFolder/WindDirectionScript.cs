using UnityEngine;
using System.Collections;

/*
 * On screen, there is an arrow in the bottom left of the screen that represents the wind direction. This script is 
 * attached to that arrow.
 */ 

public class WindDirectionScript : MonoBehaviour {
	public GameObject windzone;

	/* This script is attached to the arrow on screen. I set the rotation of this object to be equal to that of the 
	 * rotation of the windzone object. This way, the arrow always points in the direction the wind is blowing
	 */ 
	void Update ()
	{
		transform.rotation = windzone.transform.rotation;
	}
}
