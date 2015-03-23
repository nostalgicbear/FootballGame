using UnityEngine;
using System.Collections;

/*
 * This script is placed on the defenders. It controls what they do, and what state they are in.
 */ 

public class DefenderScript : MonoBehaviour {
	
	public string Name;	// name of the defender

	/* Here I create an enum that holds a load of different states for the defender. I use states throughout this
	 * project as its a very easy and clear way to control a character without needing loads of long if statements.
	 * A defedner can be either RESTING, which I should have called IDLE, RUNNING, or PASSING.
	 */
	public enum Defender_State { 
		RESTING,
		RUNNING,
		PASSING
	};
	
	public Defender_State state; // The state variable allows me to control the defender state.
	public GameObject sphere;
	public GameObject player;
	public Vector3 initial_Position;
	public CapsuleCollider capsuleCollider;

	// Use this for initialization
	void Start () {
		initial_Position = transform.position;
		state = Defender_State.RESTING; //I initially set the defender to be idle
		animation["defenderIdle"].speed = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

		/*I use a switch statement to determine what the defender should do in each state
		 */ 
		switch (state) {
			
		case Defender_State.RESTING:

			/* If the player is IDLE, I make sure the capsule collider is correctly facing up along the Y axis. This
			 * is so I can use the players as a wall to try and block the user from shooting. I then check to make sure
			 * the correct animation is playing. If the defenderIdle animation is not playing, I play it.
			 * I then make sure the defender looks at the player
			 */ 
			capsuleCollider.direction = 1;
			if ( !animation.IsPlaying("defenderIdle") )
				animation.Play("defenderIdle");
			
			transform.LookAt( new Vector3( player.transform.position.x, transform.position.y , player.transform.position.z)  );
			
			float distanceBall = (transform.position - sphere.transform.position).magnitude;
			
			if ( distanceBall < 10.0f ) {
				state = DefenderScript.Defender_State.RESTING;
			} 
			break;
		}
	}

	// to know if GoalKeeper is touching Ball
	void OnCollisionStay( Collision coll ) {
		//if ( Camera.main.GetComponent<InGameState_Script>().state == InGameState_Script.InGameState.PLAYING ) {
		}
}
