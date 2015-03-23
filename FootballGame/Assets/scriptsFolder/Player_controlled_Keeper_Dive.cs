using UnityEngine;
using System.Collections;

/*This is placed on the goalkeeper in the GoalKeeer Mode level. The goalkeeper is controlled by the player and so this 
 * script controls the Goalkeeper.
 */ 

public class Player_controlled_Keeper_Dive : MonoBehaviour {

	/*
	 * The variable below is a reference to the Player_Controoled_Keeper script which holds all the logic for the 
	 * goalkeepers state machine
	 */ 
	public Player_Controlled_Keeper goalKeeper;
	public GameObject ball;
	
	void Update() {

		/*
		 * Below I store the goalkeepers forward vector and also a reference to the balls velocity
		 */
		Vector3 dir_goalkeeper = goalKeeper.transform.forward;
		Vector3 dir_ball = ball.GetComponent<Rigidbody>().velocity;
		dir_ball.Normalize();
		
		/*This allows me to return the arc cosine of the result of the dot prouct between goalkeeper direction and ball direction
		 * I can then check if they are facing the same direction of perpendicular
		 */ 
		float det = Vector3.Dot( dir_goalkeeper, dir_ball );
		
		float degree = Mathf.Acos(det) * 57.0f;
		
		/*If the A button is pressed, or the player pushes left on the xbox controller, the keepr will
		 * change his state and jump to the left
		 */ 
		if(Input.GetKeyDown(KeyCode.A) || Input.GetAxisRaw("Horizontal") < -0.7f)
		{
			goalKeeper.state = Player_Controlled_Keeper.GoalKeeper_State.JUMP_LEFT_HIGH;
			goalKeeper.gameObject.animation.Play("playerDiveLeftLow");
		}
				
		/*If the A button is pressed, or the player pushes left on the xbox controller, the keepr will
				 * change his state and jump to the right
				 */ 
		if(Input.GetKeyDown(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0.7f)
		{
			goalKeeper.state = Player_Controlled_Keeper.GoalKeeper_State.JUMP_RIGHT_HIGH;
			goalKeeper.gameObject.animation.Play("playerDiveRightLow");
		}
	}
}
