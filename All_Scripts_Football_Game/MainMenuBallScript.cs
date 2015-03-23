using UnityEngine;
using System.Collections;

/*
 * This script is only on the ball in the main menu
 */ 

public class MainMenuBallScript : MonoBehaviour {

	public GameObject Wp1; //the balls first waypoint
	public GameObject Wp2; //the balls second waypoint
	public float ballSpeed = 10.0f;

	private GameObject target;
	private float distance; //distance from the ball to the target/ The target is one of two waypoints
	
	void Start()
	{
		float time = 0;
		target = Wp1;
	}
	
	void Update()
	{
		/*
		 *The distance from the ball to the target is stored. The ball moves between 2 waypoints. At any given time, 
		 *one of these waypoints is the balls target. When it reaches one waypoint, I set the other to be the target.
		 *This makes the ball move endlessly between the two targets. THe targets are placed at the feet of two players
		 *who kick the ball. The players play a "kick" animation when the ball gets near.
		 */
		distance = Vector3.Distance(transform.position, target.transform.position);

		/*
		 *This code below just changes the direction of the ball when it reaches the waypoint. It makes the change
		 *smoothly using Quaternion.Slerp function.
		 */
		Vector3 lookDirection = target.transform.position - transform.position;
		lookDirection.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 50);
		if(Vector3.Distance(transform.position, Wp1.transform.position) < 1)
		{
			target = Wp2;
		}
		
		if(Vector3.Distance(transform.position, Wp2.transform.position) < 1)
		{
			target = Wp1;
		}

		/*
		 *Moves the ball by applying speed
		 */
		transform.position+= transform.forward * ballSpeed * Time.deltaTime;;
	}
}





