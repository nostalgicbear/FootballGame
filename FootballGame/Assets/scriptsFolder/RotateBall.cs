using UnityEngine;
using System.Collections;

/*
 * This is placed on the ball on the main menu screen. It rotates the ball
 */

public class RotateBall : MonoBehaviour {

	private float speed = 30.0f;

	/*
	 * Roatates the ball on the Main Menu screen
	 */ 
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}
}
