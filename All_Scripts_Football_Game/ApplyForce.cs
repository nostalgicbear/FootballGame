using UnityEngine;
using System.Collections;

/*
 * This script is attached to the ball.
 */ 

public class ApplyForce : MonoBehaviour {

	private Vector3 frictionForce;
	private bool collidingWithTerrain;
	private float amountOfFriction = -0.5f;
	

	/*
	 * If the ball is inside this trigger, then it is colliding with the terrain, and the "collidingWithTerrain"
	 * variable is set to true.
	 */ 
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "TerrainCollider")
		{
			collidingWithTerrain = true;

		}
	}

	/*
	 *If the ball is not colliding with the terrain, the "collidingWithTerrain" variable is set to false. I use this
	 * when determining to apply friction or not.
	 */ 
	void OnTriggerExit(Collider col){
		if(col.gameObject.name == "TerrainCollider")
		{
			collidingWithTerrain = false;
		}
	}


	void Update () {
		/*
		 *If the ball is touching the terrain, friction is applied so it gradually comes to a stop like in real life
		 */
		if(collidingWithTerrain)
		{
			frictionForce = gameObject.rigidbody.velocity * amountOfFriction;
			gameObject.rigidbody.AddForce(frictionForce);
		}

	}
	
}
