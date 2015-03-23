using UnityEngine;
using System.Collections;

public class SwitchCameras : MonoBehaviour {
	public Camera mainCamera; //the camera attached to the Player Controller
	public Camera shotCamera; // The camea up in the air
	public Camera goalCamera; //The camera by teh goal
	public GameObject ball;
	private GameObject player;
	public float shotStrength;
	public GUIText cameraCurrentlySelected;

	private int totalCameras;
	private int activeCamera;
	private Camera currentCamera;
	private bool ballBeenKicked;

	/*
	 *Here I created an enum that has different camera states. I then use a switch statement in my update to determine
	 *which camera is active.
	 */
	private enum CameraSelected {
		MAINCAMERA,
		SHOTCAMERA,
		GOALCAMERA
	};

	private CameraSelected cameraSelected;

	// Use this for initialization
	void Start () {
		activeCamera = 1;
		ballBeenKicked = GetComponent<launcher>().kickedBall; //I get the kickedBall variable so I can change cameras once the ball is kicked
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		/*
		 * Players can cycle through cameras by pressing F. By pressing F, the activeCamera variable cycles between
		 * 1-3, activating different cameras.
		 */
		if(Input.GetKeyUp(KeyCode.F) || Input.GetButtonUp("xbox_rightBumper"))
		{
			activeCamera +=1;
		}

		/*
		 * If activeCamera is greater than 3, then it goes back to the first camera.
		 */
		if(activeCamera > 3)
		{
			activeCamera = 1;
		}

		if(activeCamera == 1)
		{
			cameraSelected = CameraSelected.MAINCAMERA;
		}
		if(activeCamera == 2)
		{
			cameraSelected = CameraSelected.SHOTCAMERA;
		}
		if(activeCamera == 3)
		{
			cameraSelected = CameraSelected.GOALCAMERA;
		}

		/*
		 * Depending on the activeCamera variable, any of the different states below can be active. THis is more
		 * efficient than having loads of if statements as they are not being regularly checked. So, there are 3 
		 * states. The main camera is the first person camera. The shot cam is a cam in the air, and the goal cam is 
		 * a camera just behind the goal. Whenever one is active the others are disabled. 
		 */ 
		switch(cameraSelected){
		case CameraSelected.MAINCAMERA:

			mainCamera.camera.enabled = true;
			currentCamera = mainCamera;
			shotCamera.camera.enabled = false;
			goalCamera.camera.enabled = false;

			cameraCurrentlySelected.text = "Main Camera Selected. Press F to change camera";

			/*
			 *Here I get the kickedBall variable from the launcher script. I tried to store this is a temporary variable
			 *but for some reason it wasnt working. I know that would be more efficient. Anyway, if kickedBall is true, 
			 *then the player has kicked the ball, so I switch to a different camera straight away once the shot is taken
			 */
			if(GetComponent<launcher>().kickedBall == true)
			{
				activeCamera = 3;
				
			}
			break;

			//The camera up in the air is selected
		case CameraSelected.SHOTCAMERA:
			mainCamera.camera.enabled = false;
			shotCamera.camera.enabled = true;
			goalCamera.camera.enabled = false;
			currentCamera = shotCamera;

			cameraCurrentlySelected.text = "Shot Camera Selected. Press F to change camera";

			/*
			 *If this camera is selected, and the player has not kicked the ball, I make the camera follow the player 
			 *as they move around. This way they can not get lost off screen if they want to run around the pitch as the 
			 *camera will always follow them.
			 */
			if(GetComponent<launcher>().kickedBall == false)
			{
				currentCamera.transform.LookAt(player.transform.position);
			}

			/*
			 *If this camera is selected and the player DOES kick the ball, then the camera follows the ball instead;
			 */
			if(GetComponent<launcher>().kickedBall == true)
			{
					currentCamera.transform.LookAt(ball.transform.position);
			}
			break;

			//This works in exactly the same way as the SHOTCAMERA above.
		case CameraSelected.GOALCAMERA:
			mainCamera.camera.enabled = false;
			shotCamera.camera.enabled = false;
			goalCamera.camera.enabled = true;
			currentCamera = goalCamera;

			cameraCurrentlySelected.text = "Goal Camera Selected. Press F to change camera";

			if(GetComponent<launcher>().kickedBall == false)
			{
				currentCamera.transform.LookAt(player.transform.position);
			}

			if(GetComponent<launcher>().kickedBall == true)
			{
				currentCamera.transform.LookAt(ball.transform.position);
			}
			break;
		}
	}
}
