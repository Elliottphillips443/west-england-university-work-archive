using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController_test : MonoBehaviour
{
	public BoxCollider2D box;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	Collision2D OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			coll.gameObject.SendMessage ("damage", 0.1f);
			print("HIT PLAYER");
		}

		return coll;
	}
}