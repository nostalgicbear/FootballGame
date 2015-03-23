using UnityEngine;
using System.Collections;

/*
 * This script is on the defenders in the main menu.
 */ 
public class MainMenuDefender : MonoBehaviour {
	
	public GameObject Wp1; //the players first waypoint
	public GameObject Wp2; // the players second waypoint
	public float playerSpeed = 10.0f; //the speed the player runs

	private GameObject target; //the waypoint that is the current target
	private float distance; // the distance between the player and their target waypoint
	
	void Start()
	{
		target = Wp1; //when the scene starts, the players run toward their first waypoint
	}
	
	void Update()
	{
		/*
		 * Here I make sure that the players name is a Defender, and if it is, the "run" animation is called.
		 */ 
		if(gameObject.name == "Defender")
		{
			animation.Play("run");
		}
		distance = Vector3.Distance(transform.position, target.transform.position);

		/*
		 *Below, I make sure the player is always looking towards their target. By using Quaternion.Slerp, when the player
		 *reaches their target, they do a nice natural looking change of direction instead of a really quick turn
		 */
		Vector3 lookDirection = target.transform.position - transform.position;
		lookDirection.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 4);

		/*
		 * If ther player reaches the first waypoint, their target becomes the second waypoint. So they then start
		 * running toward this
		 */ 
		if(Vector3.Distance(transform.position, Wp1.transform.position) < 1)
		{
			target = Wp2;
		}

		/*
		 * If ther player reaches the second waypoint, their target becomes the first waypoint. So they then start
		 * running toward this
		 */ 
		if(Vector3.Distance(transform.position, Wp2.transform.position) < 1)
		{
			target = Wp1;
		}
		
		transform.position+= transform.forward * playerSpeed * Time.deltaTime;;
	}
}





