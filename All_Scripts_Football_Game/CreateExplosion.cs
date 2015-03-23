using UnityEngine;
using System.Collections;

/* THis is placed on the crossbar and the goal posts. If the ball collides with any of them and its power is 80 or 
 * greater, then I instantiate an explosion
 */ 

public class CreateExplosion : MonoBehaviour {
	
	private float radius = 10.0f;
	private float power = 20.0f;

	public GameObject explosionPrefab; // THe prefab I instantiate to represent an explosion

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Ball")
		{
			/*If the ball has collided with the crossbar or goal posts, I check the power of the shot. If the power
			 * is 8.0 (Its measured out of 10) or higher, then I instantiate an explosion. This explosion affects rigidbodies
			 * nearby
			 */ 
			if(GameObject.Find("Launcher").GetComponent<launcher>().powerOfShot > 8.0f)
			{
				Vector3 explosionPosition = transform.position;
				Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
				Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
				foreach(Collider col in colliders)
				{
					if(col.rigidbody)
					{
						col.rigidbody.AddExplosionForce(power, explosionPosition, radius, 5.0f);
					}
				}
			}
		}
	}
}
