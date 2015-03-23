using UnityEngine;
using System.Collections;

/*
 * This is placed on the camera on the main menu screen, the end screen, and the game over screens. It makes the camera
 * look at the centre of the pitch and rotate
 */

public class RotateCamera : MonoBehaviour {
	private GameObject centerCircle; // This represents the centre of the pitch
	private float speed = 2;
	
	// Use this for initialization
	void Start () {
		centerCircle = GameObject.Find ("CenterCircle");
	}
	
	/*
	 * The camera rotates while always looking at the center of the pitch
	 */ 
	void Update () {
		transform.LookAt(centerCircle.transform);
		transform.Translate(Vector3.right * Time.deltaTime * speed);
	}
}
