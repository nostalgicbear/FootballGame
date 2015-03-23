using UnityEngine;
using System.Collections;

/*
 * This is placed on a gameobject I use to represent a wind area. The windzones that can be added via
 * GameObject > Create Other > Windzone only affect trees and so instead I use a force to represent wind
 */ 

public class ApplyWindForce : MonoBehaviour {

	private bool applyWindEffect; //Decides whether or not I will apply the wind force to an object
	private GameObject ball;

	public float windStrength = 1.0f;

	// Use this for initialization
	void Start () {
		ball = GameObject.Find("Ball");
	}
	
	/* I use fized update as its correct to use that when effecting phyiscs of an object. If the ball has a magnitude
	 * that is slightly greater than zero, then I apply a force pushing it to the left. This is because when I have 
	 * wind it my levels, it blows across the pitch to the left.
	 */ 
	void FixedUpdate () {
		if(applyWindEffect)
		{
			if(ball.rigidbody.velocity.magnitude > 0.3f)
			{
				ball.rigidbody.AddRelativeForce(new Vector3(-1,0,0) * windStrength);
			}
		}
	}

	/* If the ball enters the trigger, it is in the windzone and I know to apply wind to it.
	 */ 
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			applyWindEffect = true;
		}
		
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			applyWindEffect = false;
		}
		
	}

}
