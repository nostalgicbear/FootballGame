using UnityEngine;
using System.Collections;

/*
 * This script is on the goalkeepr that moves side to side.
 */ 
public class Goalkeeper_SideToSideMovement : MonoBehaviour {

	public GameObject Wp1; //the players first waypoint
	public GameObject Wp2; // the players second waypoint
	public float playerSpeed = 0.16f;
	
	private float distance; // the distance between the keeper and their target waypoint
	private GameObject target;
	
	void Start()
	{
		target = Wp1; //when the scene starts, the keeper moves toward their first waypoint
	}
	
	void Update()
	{
		/*
		 * Here I make sure that the animation for moving sideways is playing on the keeper
		 */ 
		if(gameObject.name == "GoalKeeperSideToSide")
		{
			animation.Play("Move_Sideways");
		}
		distance = Vector3.Distance(transform.position, target.transform.position);

		//Make him always face the player
		transform.LookAt(GameObject.Find("First Person Controller").transform.position);
		
		/*
		 * If the keeper reaches the first waypoint, his target becomes the second waypoint. So he then starts
		 * moving toward this
		 */ 
		if(Vector3.Distance(transform.position, Wp1.transform.position) < 1)
		{
			target = Wp2;
		}
		
		/*
		 * If ther keeper reaches the first waypoint, his target becomes the second waypoint. So he then starts
		 * moving toward this
		 */ 
		if(Vector3.Distance(transform.position, Wp2.transform.position) < 1)
		{
			target = Wp1;
		}
		//Moves the player side to side at a regular interval
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, playerSpeed);
	}
}





