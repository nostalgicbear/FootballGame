using UnityEngine;
using System.Collections;

/* This is the moaster script. Its the most important script in the game as I have a lot of things attached to it, and 
 * it does the majority of the work. It is attached the the Launcher object which is a child object of the First Person Controller
 * It can be found at First Person Controller > camera 3 > Launcher
 */ 

public class launcher : MonoBehaviour {
	
	public GameObject ball;
	public AudioClip kickSound;
	public float leftSwerve;
	public float rightSwerve;
	public float shotHeight;
	public float powerOfShot;
	public GameObject thePlayer;
	public float hSliderValue = 0.0f; //values for the horizontal slider that is used to determine shot power
	public float maxSliderValue = 10.0f;
	public float minSliderValue = 1.0f;
	public Transform[] shotPositions; //An array that holds all the positions that the ball will spawn from
	public GUITexture shotsRemaining1; //These are all GUI textures that tell the player how many shots they have left
	public GUITexture shotsRemaining2;
	public GUITexture shotsRemaining3;
	public GUITexture shotsRemaining4;
	public GUITexture shotsRemaining5;
	public bool kickedBall = false;
	public GUIText low; //texture that represents low shot height
	public GUIText medium; //texture that represents medium shot height
	public GUIText high; //texture that represents high shot height
	public GUIText instructions;
	public GUIText objective; //This guiText displays the objective for the level

	private AudioSource audioSource;
	private float distance; //distance from the player to the ball
	private float target; //the target value for the slider to increment towards
	private float value; //the value at which the slider increments
	private int shotsRemaining; 
	private float timer; //a timer that is used in the CheckIsBallKicked() function that determines if the ball is to be reset
	private int numberOfShotsTaken = 0;
	private bool InstructionsDisplayed = false; //a bool that controls if the instructions are displayed
	private Color shotTakenColor;
	private int activeHeight; //the currently selected height that the shot will be. Eg, low, medium, high
	private GUIText debriefText; //displayed after all shtos have been taken	
	private Color heightNotSelected; //The color that the gui texture will be if its not selected
	private bool gamePaused = false; 
	private bool slowMotion = false;//Used to determine whether or not to use slow motion

	/*This enum allows me to have different states for when either 0,1,2,3,4 or 5 shots remain. I can then adjust things
	 * such as GUI elements to display different things for each state
	 */
	private enum Shots_Remaining {
		FIVE_REMAINING,
		FOUR_REMAINING,
		THREE_REMAINING,
		TWO_REMAINING,
		ONE_REMAINING,
		NONE_REMAINING
	};

	/*I also use an enum so I can have different states for when a shot is selected to be either LOW, MEDIUM, or HIGH
	 */
	private enum Height_Selected {
		LOW,
		MEDIUM,
		HIGH
	};

	private Shots_Remaining state;
	private Height_Selected height;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f; //Set this to 1 so the game plays in realtime
		state = Shots_Remaining.FIVE_REMAINING; //The player states of having 5 shots
		height= Height_Selected.LOW; // The default selected height is LOW
		audioSource = GetComponent<AudioSource>();
		thePlayer = GameObject.Find("First Person Controller");
		hSliderValue= 0.0f; // The power bar slider starts off at 0.0 initially
		target = maxSliderValue; 
		value = 1.0f; //THis is the value at which the power bar will increase
		shotsRemaining = 5;
		ball.transform.position = GameObject.Find("BallSpawnPosition" + numberOfShotsTaken).transform.position; //set the balls position
		activeHeight = 1;
	}
	
	// Update is called once per frame
	void Update () {

		/*
		 * If this bool is true, it means that the power of teh shot is at least 9.8/10. When this is the case, the 
		 * shot is taken in slow motion for added effect.
		 */ 
		if(slowMotion)
		{
			Time.timeScale = 0.4f;
		}

		if(!slowMotion)
		{
			Time.timeScale = 1.0f;
		}

		/*The distance between the player and the ball. If the player is close enough to the ball, they
		 * can kick it. The PerformShot() functions is called. 
		 */ 
		distance = Vector3.Distance(thePlayer.transform.position, ball.transform.position);
		if(Input.GetMouseButtonUp(0) || Input.GetButtonUp("xbox_a"))
		{
			if(distance <=5)
			{
				PerformShot();
			}
		}

		/*
		 * Pressing M toggles the sound on and off in the game. So does pressing x on xbox controller
		 */ 
		if(Input.GetKeyUp(KeyCode.M))
		{
			AudioListener.pause = !AudioListener.pause;
		}

		/*
		 *This displays and hides the objective gui that is displayed in the bottom left hand corner of the screen
		 */

		if(Input.GetKeyUp(KeyCode.B))
		{
			objective.enabled = !objective.enabled;
		}

		/*
		 * If the player presses Escape, it brings them to the main menu
		 */
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.LoadLevel("MainMenu");
		}

		/*
		 * If the player pressess I, it displays the Instructions. Can also press x on the xbox controller
		 */ 
		if(Input.GetKeyUp(KeyCode.I) || Input.GetButtonUp("xbox_x"))
		{
			InstructionsDisplayed = !InstructionsDisplayed;
		}

		/*
		 * Pressing P or pressing the Start button on the xbox controller pasuses the game. I discovered that if you
		 * have an interactive cloth in your game when you pause, it will cause the game to mess up and run at less
		 * than 1 frame per second, so I use 2 methods I created called DisableAllCloths() and EnableAllCloths() to
		 * disable and enable clothes while pausing and unpausing.
		 */ 
		if(Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("xbox_pause"))
		{
			gamePaused = !gamePaused;
		}

		if(gamePaused)
		{
			DisableAllCloths();
		}
		/*
		 * Here I make sure the game is not running in slow motion before I call EnableAllCloths() as this function
		 * also affects the speed at which the game is run
		 */ 
		if(!gamePaused && !slowMotion)
		{
			EnableAllCloths();
		}

		/*This is the bool that toggles whether the instructions are displayed or not
		 */ 
		if(InstructionsDisplayed)
		{
			instructions.guiText.enabled = true;
		}

		if(!InstructionsDisplayed)
		{
			instructions.guiText.enabled = false;
		}

		/*Once the player has taken all their shots, I call the EvaluatePlayer() method to see how they did. The 
		 * EvaluatePlayer() method will determine whether they have passed the level or not.
		 */
		if(shotsRemaining <=0)
		{
			EvaluatePlayer();
		}
		
		/*If the ball has been kicked, I start a timer that lasts for 4 seconds. This gives the ball enough time to travel
		*towards the goal. Then, once the ball has stopped rolling, it is reset to the next position where the player takes
		*their next shot from
		*/

		CheckIsBallKicked();

		/*Depending on how many shots the player has taken, I change the state.
		 */
		if(numberOfShotsTaken == 0)
		{
			state = Shots_Remaining.FIVE_REMAINING;
		}
		else if(numberOfShotsTaken == 1)
		{
			state = Shots_Remaining.FOUR_REMAINING;
		}
		else if(numberOfShotsTaken == 2)
		{
			state = Shots_Remaining.THREE_REMAINING;
		}
		else if(numberOfShotsTaken == 3)
		{
			state = Shots_Remaining.TWO_REMAINING;
		}
		else if(numberOfShotsTaken == 4)
		{
			state = Shots_Remaining.ONE_REMAINING;
		}
		else if(numberOfShotsTaken == 5)
		{
			state = Shots_Remaining.NONE_REMAINING;
		}

		/*If the ball somehow falls throw the ground and ends up out of the stadium, once it falls below the stadium,
		 * NextShot() will be called, and the ball will be positioned ready for the next shot to be taken.
		 */
		if(ball.transform.position.y <= -5)
		{
			NextShot();
		}

		/*This calls the PowerBar() function. This function moves the power slider from its minimum value to the maximum
		 * value and vise versa
		 */ 
		PowerBar();

		//right click to select height
		if(Input.GetMouseButtonUp(1) || Input.GetButtonUp("xbox_b"))
		{
			activeHeight += 1;
		}

		if(activeHeight > 3)
		{
			activeHeight = 1;
		}

		if(activeHeight == 1)
		{
			height = Height_Selected.LOW;
		}
		if(activeHeight == 2)
		{
			height = Height_Selected.MEDIUM;
		}
		if(activeHeight == 3)
		{
			height = Height_Selected.HIGH;
		}

		/*This switch statement lets me control the height that is selected by the player. If the selected height is 
		 * HIGH, then the MEDIUM, and LOW options are grayed out so it is clear which has been selected. THis is done
		 * for the LOW and MEDIUM cases too. I also store the height of the shot so it can be used in 
		 * the PerformShot() function when the shot is taken. 
		 */ 
		switch(height){
		case Height_Selected.HIGH:
			heightNotSelected = low.color;
			heightNotSelected = new Color(47,47,47);
			heightNotSelected.a = 0.1f;
			low.color = heightNotSelected;

			heightNotSelected = medium.color;
			heightNotSelected = new Color(47,47,47);
			heightNotSelected.a = 0.1f;
			medium.color = heightNotSelected;

			heightNotSelected = high.color;
			heightNotSelected = new Color(128,128,128);
			heightNotSelected.a = 1.0f;
			high.color = heightNotSelected;

			shotHeight = 2.0f;
			break;
			
		case Height_Selected.MEDIUM:
			heightNotSelected = low.color;
			heightNotSelected = new Color(47,47,47);
			heightNotSelected.a = 0.1f;
			low.color = heightNotSelected;
			
			heightNotSelected = high.color;
			heightNotSelected = new Color(47,47,47);
			heightNotSelected.a = 0.1f;
			high.color = heightNotSelected;

			heightNotSelected = medium.color;
			heightNotSelected = new Color(128,128,128);
			heightNotSelected.a = 1.0f;
			medium.color = heightNotSelected;

			shotHeight = 1.0f;
			break;
			
		case Height_Selected.LOW:
			heightNotSelected = high.color;
			heightNotSelected = new Color(47,47,47);
			heightNotSelected.a = 0.1f;
			high.color = heightNotSelected;

			heightNotSelected = medium.color;
			heightNotSelected = new Color(47,47,47);
			heightNotSelected.a = 0.1f;
			medium.color = heightNotSelected;

			heightNotSelected = low.color;
			heightNotSelected = new Color(128,128,128);
			heightNotSelected.a = 1.0f;
			low.color = heightNotSelected;

			shotHeight = 0.2f;
			
			break;
		}

		/*This switch statement allows me to control the GUITextures in the bottom right hand corner of the screen.
		 * These GUI balls represent the amount of shots remaining. When the player takes a shot, I grey out a ball
		 * so it is clear to them how many shots they have left.
		 */
		switch(state) {
		case Shots_Remaining.FIVE_REMAINING:
				//Everything is automatically enabled on START() for this state so I neeto do nothing else
			break;
			
		case Shots_Remaining.FOUR_REMAINING:
			shotTakenColor = shotsRemaining5.color;
			shotTakenColor = new Color(47,47,47);
			shotTakenColor.a = 0.1f;
			shotsRemaining5.color = shotTakenColor;
			break;
			
		case Shots_Remaining.THREE_REMAINING:
			shotTakenColor = shotsRemaining5.color;
			shotTakenColor = new Color(47,47,47);
			shotTakenColor.a = 0.1f;
			shotsRemaining4.color = shotTakenColor;
			break;
			
		case Shots_Remaining.TWO_REMAINING:
			shotTakenColor = shotsRemaining5.color;
			shotTakenColor = new Color(47,47,47);
			shotTakenColor.a = 0.1f;
			shotsRemaining3.color = shotTakenColor;
			break;
			
		case Shots_Remaining.ONE_REMAINING:
			shotTakenColor = shotsRemaining5.color;
			shotTakenColor = new Color(47,47,47);
			shotTakenColor.a = 0.1f;
			shotsRemaining2.color = shotTakenColor;
			break;
			
		case Shots_Remaining.NONE_REMAINING:
			shotTakenColor = shotsRemaining5.color;
			shotTakenColor = new Color(47,47,47);
			shotTakenColor.a = 0.1f;
			shotsRemaining1.color = shotTakenColor;
			break;
		}
	}

	/* This places the power bar slider on screen and sets the paramters for it
	 */ 
	void OnGUI()
	{
		hSliderValue = GUI.HorizontalSlider(new Rect(Screen.width/2, Screen.height - 80, 100, 50), hSliderValue, minSliderValue, maxSliderValue);
	}

	/*
	 * Disables all interactive cloths when the player pauses the game, as they will cause the game to run at a low
	 * frame rate otherwise
	 */ 
	void DisableAllCloths()
	{
		InteractiveCloth[] cloths = FindObjectsOfType(typeof(InteractiveCloth)) as InteractiveCloth[];
		foreach(InteractiveCloth cloth in cloths)
		{
			cloth.enabled = false;
			Time.timeScale = 0.0f;
		}
	}
	/*
	 * Enables all interactive cloths after player unpauses the game
	 */ 
	void EnableAllCloths()
	{
		InteractiveCloth[] cloths = FindObjectsOfType(typeof(InteractiveCloth)) as InteractiveCloth[];
		foreach(InteractiveCloth cloth in cloths)
		{
			cloth.enabled = true;
			Time.timeScale = 1.0f;
		}
	}

	/*This is called in the Update() function. The power bar slider moves from the minimum value to the maximum value.
	 * When it reaches the maximum, its target becomes the mimimum value and so it moves towards that, and vice versa.
	 * THe "hSliderValue" variable is the horizontal slider value I use to represent the power of the shot. THis is then
	 * used when the player shoots.
	 */ 
	void PowerBar()
	{
		if(hSliderValue >= maxSliderValue)
		{
			target = minSliderValue;
			value = -1.0f;
		}
		
		if(hSliderValue <= minSliderValue)
		{
			target = maxSliderValue;
			value = 1.0f;
		}
		hSliderValue += value;
	}
	
	/*
	 * This is called if the player is in range of teh ball, and presses the Left Mouse Click button. When the player
	 * shoots, the value the power bar slider is currently at is taken and used as the strenght of the shot. The shot
	 * height is also taken and applied to the shot. This can be either LOW, MEDIUM, or HIGH. The value of shotHeight
	 * depends on which of these is selected. The ball is shot in whatever direction the player is facing. The power
	 * and height choosen by the player is applied.
	 */ 
	void PerformShot()
	{
		powerOfShot = hSliderValue / 10.0f; //Power of the shot

		if(powerOfShot >=9.8f)
		{
			slowMotion = true;
		}

		Vector3 shootingDirection = thePlayer.transform.forward * powerOfShot; 
		shootingDirection.y +=shotHeight;
		/*
		 * The rightSwerve and leftSwerve variables below add some random swerve to the ball. It is only slight, but 
		 * I think its true to life, as very few people can put a ball EXACTLY where they want
		 */ 
		rightSwerve = Random.Range(0.0f, 0.3f);
		leftSwerve = Random.Range(0.0f, 0.3f);
		ball.rigidbody.AddForce(shootingDirection, ForceMode.Impulse); //shoots the ball forward
		ball.rigidbody.AddForce(Vector3.right * rightSwerve, ForceMode.Impulse); 
		ball.rigidbody.AddForce(Vector3.right * leftSwerve, ForceMode.Impulse);
		audioSource.audio.PlayOneShot(kickSound, 1.0f); //plays a kick sound when the shot is taken
		kickedBall = true;
		numberOfShotsTaken += 1;
	}

	/*When the ball has been kicked, I wait a few seconds for the ball to stop moving and then call the NextShot()
	 * function below. I look for the next BallSpawnPosition to find out where to place the ball. The ball then resets to
	 * that specific position so the player can take the next shot from a new location.
	 */ 
	void NextShot()
	{
		slowMotion = false;
		Transform newBallPosition = GameObject.Find("BallSpawnPoint" + numberOfShotsTaken).transform;
		ball.transform.position = newBallPosition.position;
		ball.rigidbody.velocity = new Vector3(0,0,0);

		/*If for some reason, a position hasnt been specified, I reset the ball to the centre circle
		 */ 
		if(newBallPosition == null)
		{
			newBallPosition.position = new Vector3(0,1,0); //reest the ball to the centre circle if the next shot position isnt found
		}
	}

	/*If the player scored at least 4/5, then they can progress to the next level. I store the index of the current 
	 * level, and if the player has scored enough goals, I load the next level. If the player has not scored enough
	 * then I load the Game Over screen. I also display a message that tells them if they won or lost
	 */ 
	void EvaluatePlayer()
	{
		int i = Application.loadedLevel; //stores the index of the current level. This is displaued under build options
		float playerScore = GameObject.Find("GoalTrigger").GetComponent<UpdateScore>().score; //get the players score
		if(playerScore >=4)
		{
			GameObject.Find("DebriefGUI").GetComponent<GUIText>().text = "You passed. Moving on to next level";
			Application.LoadLevel(i+1);
		}
		if(playerScore <=3){
			GameObject.Find("DebriefGUI").GetComponent<GUIText>().text = "You didnt pass the level";
			Application.LoadLevel("GameOver_Lose");
		}
	}
	/*This function checks if the ball has been kicked. If it has, it waits 7 seconds, and then it calls
	 * the NextShot() function which places the ball to its next position for the next shot
	 */ 
	void CheckIsBallKicked()
	{
		if(kickedBall)
		{
			timer+= Time.deltaTime;
			if(timer >= 7.0f)
			{
					shotsRemaining -=1;
					kickedBall = false;
					NextShot();
					timer = 0.0f;
			}
		}
	}
	
}
