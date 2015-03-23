using UnityEngine;
using System.Collections;

/*This is placed on both the bottom left triger, and bottom right trigger attached to the goalkeeper. THese are the 
 * triggers that determine whether the keeper should dive low to the left or dive low to the right
 */ 

public class GoalKeeperJump_Down : MonoBehaviour {

	public GoalKeeper_Script goalKeeper;

	void OnTriggerEnter( Collider other ) {
	
		if ( other.tag == "Ball" ) {
		
			/*
			 * If the ball collides with either the bottom left trigger, or the bottom right trigger, the goalkeeer dives to save the ball. This is 
			 * done by using vectors to determine the arc cosine of the dot product between the balls direction vector and 
			 * the goalkeepers direction vector.
			 */
			Vector3 dir_goalkeeper = goalKeeper.transform.forward;
			Vector3 dir_ball = other.gameObject.GetComponent<Rigidbody>().velocity;
			dir_ball.Normalize();

			/*This allows me to return the arc cosine of the result of the dot prouct between goalkeeper direction and ball direction
			 * I can then check if they are facing the same direction of perpendicular
			 */ 
			float det = Vector3.Dot( dir_goalkeeper, dir_ball );
						
			float degree = Mathf.Acos(det) * 57.0f;
			
			if (degree > 90.0f && degree < 270.0f && other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 5.0f && !other.gameObject.GetComponent<Rigidbody>().isKinematic)
			{	
				/*This trigger has been tagged "GoalKeeper_Jump_Left". If the ball vollides with the trigger and is a moving object
				 * I change the goalkeepers state
				 */ 
				if (tag == "GoalKeeper_Jump_Left")
				{
					goalKeeper.state = GoalKeeper_Script.GoalKeeper_State.JUMP_LEFT_HIGH;
					goalKeeper.gameObject.animation.Play("playerDiveLeftLow");
				}
	
				/*This trigger has been tagged "GoalKeeper_Jump_Right". If the ball collides with the trigger and is a moving object
				 * I change the goalkeepers state
				 */ 
				if (tag == "GoalKeeper_Jump_Right")
				{
					goalKeeper.state = GoalKeeper_Script.GoalKeeper_State.JUMP_RIGHT_HIGH;
					goalKeeper.gameObject.animation.Play("playerDiveRightLow");
				}
			}
		}
	}
}
