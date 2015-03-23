using UnityEngine;
using System.Collections;

/*This script is on a GUIText variable used to represent the time
 */

public class Timer : MonoBehaviour {
	private float seconds = 59;
	private float minutes = 3;

	// Update is called once per frame
	void Update () 
	{
		/*
		 * This just reduces the seonds and the minutes correctly
		 */ 
		if(seconds <=0)
		{
			seconds = 59;
			if(minutes >=1)
			{
				minutes --;
			}
			else{
				minutes = 0;
				seconds = 0;
				GetComponent<GUIText>().text = minutes.ToString("f0") + ":0" + seconds.ToString("f0");
			}
		}
		else{
			seconds -= Time.deltaTime;
		}
		/*
		 * Here I make the format of the time X.XX. It makes sure that the time is not displayed as something like 
		 * 3.56.7878787. In this instance it would round to 3.56 making it much easier to read
		 */ 
		if(Mathf.Round(seconds) <=9)
		{
			GetComponent<GUIText>().text = minutes.ToString("f0") + ":0" + seconds.ToString("f0");
		}
		else{
			GetComponent<GUIText>().text = minutes.ToString("f0") + ":" + seconds.ToString("f0");
		}

		/*
		 * If time runs out, I then load the game over screen
		 */ 
		if(minutes <=0 && seconds <=0)
		{
			GameObject.Find("DebriefGUI").GetComponent<GUIText>().guiText.text = "You ran out of time. You lose";
			Application.LoadLevel("GameOver_Lose");
		}
	}
	
}
