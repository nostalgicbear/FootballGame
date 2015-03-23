using UnityEngine;
using System.Collections;

public class moveKeeper : MonoBehaviour {
	private Vector3 wp1;
	private Vector3 wp2;
	private int wpCounter;
	private GameObject target;

	// Use this for initialization
	void Start () {
		wpCounter = 1;
	
	}
	
	// Update is called once per frame
	void Update () {
		target = GameObject.Find("wp" + wpCounter);
		if(Vector3.Distance(gameObject.transform.position, target.transform.position) < 0.5f)
		{
			wpCounter++;
		}

		if(wpCounter > 2)
		{
			wpCounter = 1;
		}

	
	}
}
