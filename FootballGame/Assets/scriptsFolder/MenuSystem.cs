using UnityEngine;
using System.Collections;

/*
 * This script is used to navigate the main menu. It is placed on all the menu options/
 */ 

public class MenuSystem : MonoBehaviour {
	private GUIText thisText; //This stores the text of GUIText. Eg. The text for the "Level1" gui object would contain "Level1 " so that would be stored
	private Color originalColor; //The original color of the text
	private Color highlightedColor; //the color I want to change the text to when its highlighted
	private string levelToLoad;
	private int maxFontSize = 25;
	private GameObject instructionsList; //This is a list of instructions of how to play the game
	private bool instructionsEnabled; //A bool controlling whether or not the instructions are displayed
	
	// Use this for initialization
	void Start () {
		thisText = GetComponent<GUIText>();
		originalColor = thisText.color;
		highlightedColor = new Color(1,0,1); //Here I set the color I want to change the text to when highlighted
		instructionsEnabled = false;
		GameObject.Find("InstructionsList").GetComponent<GUIText>().enabled = false; //Do not display instructions
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * If the mouse hovers over any of the menu options, I set the color of the text to the color stored in the
		 * highlightedText variable. I also increase the font size slightly. I increase the size but limit it to
		 * the size stored in the maxFontSize variable. This gives a really nice effect when a menu option is selected
		 * and the player can clearly see which option is being hovered over with the cursor.
		 */
		if(thisText.HitTest(Input.mousePosition))
		{
			thisText.color = highlightedColor;
			thisText.fontSize +=1;
			if(thisText.fontSize > maxFontSize)
			{
				thisText.fontSize = maxFontSize;
			}
			if(Input.GetMouseButtonDown(0)) //The level that is loaded depends on which option is clicked
			{
				//levelToLoad = thisText.text;
				levelToLoad = thisText.name;
			}
		}
		else //When the mouse is moved off a menu option, it returns to its normal colorand size
		{
			thisText.color = originalColor;
			thisText.fontSize = 20;
		}
		/*
		 * This just loads a level depending on what is stored in the "levelToLoad" variable.
		 */ 
		if(levelToLoad == "Start Game")
		{
			Application.LoadLevel("Level1");
		}
		
		if(levelToLoad == "Level 1")
		{
			Application.LoadLevel("football");
		}
		else if(levelToLoad == "Level 2")
		{
			Application.LoadLevel("Level2");
		}
		else if(levelToLoad == "Level 3")
		{
			Application.LoadLevel("Level3");
		}
		else if(levelToLoad == "Level 4")
		{
			Application.LoadLevel("Level4");
		}
		else if(levelToLoad == "Level 5")
		{
			Application.LoadLevel("Level5");
		}
		else if(levelToLoad == "Level 6")
		{
			Application.LoadLevel("Level6");
		}
		else if(levelToLoad == "Level 7")
		{
			Application.LoadLevel("Level7");
		}
		else if(levelToLoad == "Level 8")
		{
			Application.LoadLevel("Level8");
		}
		else if(levelToLoad == "Level 9")
		{
			Application.LoadLevel("Level9");
		}

		else if(levelToLoad == "Quit")
		{
			Application.Quit();
		}
		else if(levelToLoad == "Target Practice")
		{
			Application.LoadLevel("TargetPractice");
		}
		else if(levelToLoad == "Main Menu")
		{
			Application.LoadLevel("MainMenu");
		}
		else if(levelToLoad == "Random Level")
		{
			Application.LoadLevel(Random.Range(1,10));
		}
		else if(levelToLoad == "Goalkeeper Mode")
		{
			Application.LoadLevel("Goalkeeper Mode");
		}
		/*
		 * If the Instructions menu option is clicked, the Instructions are displayed.
		 */ 
		else if(levelToLoad == "Instructions")
		{
			GameObject.Find("InstructionsList").GetComponent<GUIText>().enabled = true;
		}
	}
}
