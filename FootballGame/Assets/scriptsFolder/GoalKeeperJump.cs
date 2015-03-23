using UnityEngine;
using System.Collections;

/*
 * The goalkeeper has 5 colliders attached. One that represents his main body, one that triggers that he dive to the 
 * bottom right, aone that triggers he dive to the bottom left, one that triggers he dive to the top right, and one 
 * that triggers he dive to the top left. This script is placed on the triggers for the top right and left. Its how I 
 * determine that the keeper has to dive high as opposed to low when he dives to the side.
 */

public class GoalKeeperJump : MonoBehaviour {

	public GoalKeeper_Script goalKeeper;


	void OnTriggerEnter( Collider other ) {
	
		if (other.tag == "Ball")
		{
			Vector3 dir_goalkeeper = goalKeeper.transform.forward; //the firection of the keepr is his forward vector
			Vector3 dir_ball = other.gameObject.GetComponent<Rigidbody>().velocity; //direction of the ball
			dir_ball.Normalize();
			
			float det = Vector3.Dot( dir_goalkeeper, dir_ball ); //multiply the two vectors
						
			/*This allows me to return the arc cosine of the result of the dot prouct between goalkeeper direction and ball direction
			 * I can then check if they are facing the same direction of perpendicular
			 */ 
			float degree = Mathf.Acos(det) * 57.0f;
			
			if ( degree > 90.0f && degree < 270.0f && other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 5.0f && !other.gameObject.GetComponent<Rigidbody>().isKinematic) {			

				/*This trigger has been tagged "GoalKeeper_Jump_Left". If the ball collides with the trigger and is a moving object
				 * I change the goalkeepers state
				 */ 
				if (tag == "GoalKeeper_Jump_Left")
				{
					goalKeeper.state = GoalKeeper_Script.GoalKeeper_State.JUMP_LEFT;
					goalKeeper.gameObject.animation.Play("playerDiveLeftHigh");
				}

				/*
				 * Same as above
				 */ 
				if (tag == "GoalKeeper_Jump_Right")
				{
					goalKeeper.state = GoalKeeper_Script.GoalKeeper_State.JUMP_RIGHT;
					goalKeeper.gameObject.animation.Play("playerDiveRight");
				}
		
			}
		
		}
		
		
	}
	
}
