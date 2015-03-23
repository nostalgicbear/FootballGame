using UnityEngine;
using System.Collections;

/* This script is placed on the fans, and is used to control their behaviour
 */ 

public class FanScript : MonoBehaviour {
	
	public string Name;	//fan name. I didnt end up actually using this

	/*
	 *This enum allows me to control the state of each fan
	 */
	public enum Fan_State { 
		RESTING,
		CELEBRATING
	};
	
	public Fan_State state;
	public Vector3 initial_Position;
	public GameObject player;

	// Use this for initialization
	void Start () {
		initial_Position = transform.position;
		state = Fan_State.RESTING;
		animation["defenderIdle"].speed = 0.7f;
	}
	
	// Update is called once per frame
	void Update () {

		/*
		 * This switch statement controls the fans state. If they are RESTING, they are standing idle, and will always 
		 * face towards the player while the defenderIdle animation plays. I didnt feel the need to use a different idle
		 * animation for defenders and fans.
		 */ 
		switch (state) {
			
		case Fan_State.RESTING:

			if ( !animation.IsPlaying("defenderIdle") )
				animation.Play("defenderIdle");
			
			transform.LookAt( new Vector3( player.transform.position.x, transform.position.y , player.transform.position.z)  );

			break;

			/*Fans can also be in a celebrating state. This is activated when the player scores a goal. The celebration
			 * animation plays.
			 */ 
		case Fan_State.CELEBRATING:

			if ( !animation.IsPlaying("celebrate") )
				animation.Play("celebrate");
			
			transform.LookAt( new Vector3( player.transform.position.x, transform.position.y , player.transform.position.z)  );
			break;
		}
	}
}
