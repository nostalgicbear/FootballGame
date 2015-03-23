using UnityEngine;
using System.Collections;

public class UpdateScore : MonoBehaviour {

	public GUIText scoreGUI;
	public int score = 0;

	private GameObject ball;
	private GUIText goalScoredGUI;
	private bool displayGoalMessage = false;

	// Use this for initialization
	void Start () {
		/*
		 *Here I store references to the ball and to the GUI I want to affect when a goal is scored
		 */
		ball = GameObject.Find("Ball");
		goalScoredGUI = GameObject.Find("GoalScoredGUI").GetComponent<GUIText>();
	}

	/*
	 *This trigger is places inside the goal. So if the ball goes in the goal, it enters this trigger. I then update 
	 *the score, and set "displayGoalMessage" to true. When this is true, a message is displayed to let the player know
	 *they have scored.
	 */
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			score+=1;
			displayGoalMessage = true;
		}
	}

	void Update()
	{
		/*
		 *This updates the score on the players screen
		 */
		scoreGUI.text = score + " - " + "0";

		/*
		 *When the player scores a goal "displayGoalMessage" is set to true, and a message is displayed. This message 
		 *gets bigger and bigger like in the old FIFA games. When it gets to be larger than font size 80, I set it back 
		 *to false. It will only be triggered again if the player scores again.
		 */
		if(displayGoalMessage)
		{
			goalScoredGUI.enabled = true;
			goalScoredGUI.fontSize += 1;
		}
		
		if(goalScoredGUI.fontSize > 80)
		{
			goalScoredGUI.fontSize = 1;
			goalScoredGUI.enabled = false;
			displayGoalMessage = false;
		}
	}
}
