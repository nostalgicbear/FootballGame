using UnityEngine;
using System.Collections;
/*
 * This script is only in the GoalKeeper level. It is on an empty game object where the ball spawns. This script causes the ball to position itself and 
 * shoot forward. It also controls the gui for the scene, and allows the player to change camera.
 */ 

public class Ball_Spawn_Point : MonoBehaviour {

	private bool kickedBall = false;
	private float timer;
	public Camera playerCam;
	private bool goalCamActive = true;
	private int shotsLeft;
	private GUIText shotsLeftGUI;

	public Camera goalCam;
	public GameObject ball;
	public GameObject player; //player that kicks the ball. I control his animations here


	// Use this for initialization
	void Start () {
		shotsLeftGUI = GameObject.Find("ShotsRemainingGUI").GetComponent<GUIText>();
		shotsLeft = 10;
		timer = 0.0f;
		InvokeRepeating("ShootBall", 5.0f, 10.0f); //shoots the ball after 5 seconds and then every 10 secs after that
		ball.transform.position = transform.position; // set the ball to this empty objects postion.
	}

	/*
	 * This function shoots the ball forward. I add force to the ball, to shoot it forward, and I also add random 
	 * swerve to the ball. This means that it can be shot randomly left or right.
	 */ 
	void ShootBall()
	{
		player.gameObject.animation.Play("pass");
		kickedBall = true;
		shotsLeft -= 1;
		float shotHeight = 400.0f;
		float shotPower = Random.Range(12, 13);
		Vector3 ballDirection = new Vector3(transform.forward.x, shotHeight, transform.forward.z);
		ball.rigidbody.AddForce(transform.forward * shotPower, ForceMode.Impulse);
		ball.rigidbody.AddForce(Vector3.right * Random.Range(1,7), ForceMode.Impulse); //add random force right
		ball.rigidbody.AddForce(Vector3.left * Random.Range(1,7), ForceMode.Impulse); //add random force left
	}

	void Update()
	{
		/*
		 * If the ball is not kicked, the player plays the idle animation
		 */ 
		if(!kickedBall)
		{
			player.gameObject.animation.Play("defenderIdle");
		}

		shotsLeftGUI.text = shotsLeft.ToString(); //Update the number of shots remaining

		/*
		 * If the ball has been kicked, I start a timer that lasts for 7 seconds. After 7 seconds, the ball is reset.
		 */ 
		if(kickedBall = true)
		{
			timer+=Time.deltaTime;
			if(timer > 7.0f)
			{
				ball.transform.position = transform.position;
				timer = 0.0f;
				kickedBall = false;
			}
		}

		/*
		 * This allows me to swap between two cameras. If the player presses F, they can swap between the cameras.
		 * The code below simply enables one and disables the other.
		 */ 
		if(goalCamActive)
		{
			goalCam.enabled = true;
			playerCam.enabled = false;
		}
		if(!goalCamActive)
		{
			goalCam.enabled = false;
			playerCam.enabled = true;
		}

		/*
		 * Swap cameras when the F key is pressed
		 */ 
		if(Input.GetKeyDown(KeyCode.F) || Input.GetButtonUp("xbox_rightBumper"))
		{
			goalCamActive = !goalCamActive;
		}

		/*
		 * When the player has no more shots left, I call the EndLevel function using a Coroutine. This means I can
		 * tell it to wait a few saeconds, and then execute some code.
		 */
		if(shotsLeft <=0)
		{
			StartCoroutine("EndLevel");
		}
	}

	/*
	 * This is called when there are no more shots left. It displays the game over message, giving the player time to
	 * read it, and then after 8 seconds, it loads the main menu
	 */
	IEnumerator EndLevel()
	{
		GameObject.Find("EndGameGUI").GetComponent<GUIText>().enabled = true;
		yield return new WaitForSeconds(8);

		Application.LoadLevel("MainMenu");
	}


}
