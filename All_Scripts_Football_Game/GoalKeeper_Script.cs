using UnityEngine;
using System.Collections;

/*
 * This script is attached to the goalkeeper that dives. It is the main goalkeepr I use in most levels
 */ 

public class GoalKeeper_Script : MonoBehaviour {

	public string Name;	

	/*
	 * This allows me to control the different states of teh goalkeeper
	 */ 
	public enum GoalKeeper_State { 
	   RESTING, //This is when the keeper is idle
	   JUMP_LEFT,
	   JUMP_RIGHT,
	   JUMP_LEFT_HIGH,
	   JUMP_RIGHT_HIGH,
		MOVE_TO_RESTING
	};
	
	public GoalKeeper_State state;
	public GameObject sphere;
	public GameObject player;
	public Vector3 initial_Position; //goalkeeper starting position in the level
	public CapsuleCollider capsuleCollider;	// A reference to teh collider of the main body of the keeper

	// Use this for initialization
	void Start () {
		initial_Position = transform.position; //store the keepers initial position
		state = GoalKeeper_State.RESTING; //
		initial_Position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		animation["playerIdle"].speed = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		/* This switch statement lets me control the goalkeepr state.
		 */ 
		switch (state) {
	
			/* This is the JUMP_RIGHT state. When triggered, I make the goalkeeper jump to the right, and I play the 
			 * "diveRight" animation. I also turn the capsule collider on the keepr to be horizontal so it stays inline
			 * with his body. After that animation has played, and the keeper has dive, I change the state to be 
			 * MOVE_TO_RESTING, which then tells the keeper to move back to his initial position.
			 */ 
			case GoalKeeper_State.JUMP_RIGHT:

				capsuleCollider.direction = 0; //changes the collider to be horizontal when the keeper dives

				if(animation["playerDiveRight"].normalizedTime < 0.45f)
				{
					transform.position += transform.right * Time.deltaTime * 7.0f;
				}		
				if (!animation.IsPlaying("playerDiveRight"))
				{
					state = GoalKeeper_State.MOVE_TO_RESTING;
					capsuleCollider.direction = 1;
				}
			break;

			/*This is the same concept as the state above
			 */ 
		case GoalKeeper_State.JUMP_LEFT:

			capsuleCollider.direction = 0;
			if (animation["playerDiveLeftLow"].normalizedTime < 0.45f)
			{
				transform.position -= transform.right * Time.deltaTime * 7.0f;
			}

			if (!animation.IsPlaying("playerDiveLeftLow"))
			{
				state = GoalKeeper_State.JUMP_LEFT; //resting
				capsuleCollider.direction = 1;
				/*For some reason after diving to the bottom left, the keeeper would not enter the MOVE_TO_RESTING 
				 * state where he walks back to his original starting position. I dont know why. To fix this, I created 
				 * a method called "ReturnToRestState" that automatically sets the keepers state to that state after he has dived 
				 * to the left. Its called via the StartCoroutine function below. It solved the issue
				 */
				StartCoroutine("ReturnToRestState");
			}
			break;
			
		case GoalKeeper_State.JUMP_LEFT_HIGH:
				
				capsuleCollider.direction = 0;
			
				if (animation["playerDiveLeftHigh"].normalizedTime < 0.45f)
				{
					transform.position -= transform.right * Time.deltaTime * 4.0f;
				}
				
				if (!animation.IsPlaying("playerDiveLeftHigh"))
				{
					state = GoalKeeper_State.JUMP_LEFT;
					capsuleCollider.direction = 1;

				}
			break;
	
			case GoalKeeper_State.JUMP_RIGHT_HIGH:

				capsuleCollider.direction = 0;
	
				if (animation["playerDiveRightLow"].normalizedTime < 0.45f)
				{
					transform.position += transform.right * Time.deltaTime * 4.0f;
				}		
				if (!animation.IsPlaying("playerDiveRightLow"))
				{
				state = GoalKeeper_State.MOVE_TO_RESTING;
					capsuleCollider.direction = 1;
				}
			break;

			/*This state is active just after the keepr has dived. After diving, he is now out of position and must 
			 * move back to his original standing position. After he has finished diving, the keeper moves to his 
			 * starting spot which is stored in the initial_Position variable. While doing so he always looks at the 
			 * player so if the player takes their next shot fast, the goalkeepers is ready to make a save.
			 */
		case GoalKeeper_State.MOVE_TO_RESTING:

			capsuleCollider.direction = 1; //Resets the capsule collider to be vertical
			if (!animation.IsPlaying("Move_Sideways"))
			{
				animation.Play("Move_Sideways");
			}
			transform.LookAt(new Vector3(player.transform.position.x, transform.position.y , player.transform.position.z)); //face the player
			transform.position = Vector3.MoveTowards(transform.position, initial_Position, Time.deltaTime); //move back to starting position

			/*Once the keeper reaches his starting position, he changes his state to RESTING as he doesnt need to move
			 * any further
			 */
			if(Vector3.Distance(transform.position, initial_Position) < 0.2f)
			{
				state = GoalKeeper_State.RESTING;
			}
			break;
					
			/*THis is the goalkeeper idle state. He faces the player and stays still in this state.
			 */
			case GoalKeeper_State.RESTING:
			
				capsuleCollider.direction = 1;
				if ( !animation.IsPlaying("playerIdle") )
					animation.Play("playerIdle");
				
				transform.LookAt( new Vector3( player.transform.position.x, transform.position.y , player.transform.position.z)  );
			
				float distanceBall = (transform.position - sphere.transform.position).magnitude;
		
				if ( distanceBall < 10.0f ) {
					state = GoalKeeper_Script.GoalKeeper_State.RESTING;
				//come back to thus
				} 
			break;
		}
	}

	/*Sets the keepers state to MOVE_TO_RESTING
	 */ 
	IEnumerator ReturnToRestState()
	{
		yield return new WaitForSeconds(0);
		state = GoalKeeper_State.MOVE_TO_RESTING;
	}

	/*This is the collision for when the keeper saves the ball. I change the state to make him return to his original
	 * position
	 */
	void OnCollisionStay(Collision coll)
	{
		if (coll.collider.transform.gameObject.tag == "Ball" && state != GoalKeeper_State.JUMP_LEFT && state != GoalKeeper_State.JUMP_RIGHT &&
			 state != GoalKeeper_State.JUMP_LEFT_HIGH && state != GoalKeeper_State.JUMP_RIGHT_HIGH)
		{
			state = GoalKeeper_State.MOVE_TO_RESTING;
		}
	}
}
