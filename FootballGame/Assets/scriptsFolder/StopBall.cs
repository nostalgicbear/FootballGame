using UnityEngine;
using System.Collections;

/*
 * This is placed on a trigger at the back of the net
 */ 

public class StopBall : MonoBehaviour {
	private GameObject ball;
	private Vector3 stopBall; //This is a Vector that stops the ball moving

	// Use this for initialization
	void Start () {
		ball = GameObject.Find("Ball");
		stopBall = new Vector3(0,0,0); //The vector I will apply to the ball when it hits this trigger object
	}

	/*
	 * When the ball collides with this trigger, it means the ball has reached the back of the net. I then stop
	 * the balls velocity so it doesnt go through the net and go hitting various parts of the stadium. To do this
	 * I get the velocity of the ball by accesing its rigidbody and applying the vector initialized in the start function
	 */ 
	void CollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Ball")
		{
			other.gameObject.rigidbody.velocity = new Vector3(stopBall.x, stopBall.y, stopBall.z);
		}
	}
}
