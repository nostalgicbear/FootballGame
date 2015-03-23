using UnityEngine;
using System.Collections;

public class ShotPositioning : MonoBehaviour {
	public GameObject ball;
	public GameObject player;
	private GameObject startingPosition;

	public enum Shot_Number {
		FIRST_SHOT,
		SECOND_SHOT,
		THIRD_SHOT,
		FOURTH_SHOT,
		FIFTH_SHOT
	};

	public Shot_Number state;

	// Use this for initialization
	void Start () {
		state = Shot_Number.FIRST_SHOT;
	
	}
	
	// Update is called once per frame
	void Update () {

		switch(state)
		{
		
		case Shot_Number.FIRST_SHOT:

			break;
		}


	
	}
}
