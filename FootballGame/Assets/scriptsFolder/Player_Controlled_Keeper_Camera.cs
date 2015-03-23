using UnityEngine;
using System.Collections;

/*
 * This script is only present in the Goalkeeper Mode level. The script is on both the GoalCam and the PlayerCam.
 */ 

public class Player_Controlled_Keeper_Camera : MonoBehaviour {

	public Transform target;


	// Update is called once per frame
	void Update () {
		transform.LookAt(target.position); //Makes the camera focus on the players position at all times
	
	}
}
