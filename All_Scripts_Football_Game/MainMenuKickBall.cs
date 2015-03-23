using UnityEngine;
using System.Collections;

/*
 * This script is only on two players in the main menu scene. 
 */ 

public class MainMenuKickBall : MonoBehaviour {
	private float distanceToBall; //The distance from the player to the ball
	private GameObject ball;

	// Use this for initialization
	void Start () {
		ball = GameObject.Find("Ball");
		animation.Play("playerIdle"); //This plays an animation where the player stands idle
	}

	void Update () {
		/*
		 * I store the distance between the player and the ball. If the ball is far away from the player, they just 
		 * stand idle waiting for the ball to reach them. When the ball is less than 4.0f away, the "pass" animation is
		 * played, and the player kicks the ball. 
		 */ 
		distanceToBall = Vector3.Distance(transform.position, ball.transform.position);

		if(distanceToBall < 4.0f)
		{
			animation.Play("pass");
		}
		else{
			animation.Play("playerIdle");
		}
	
	}
}
