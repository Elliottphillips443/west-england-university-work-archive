using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPhysicsTest : MonoBehaviour {
    Rigidbody rb;
    [SerializeField]
    float launchSpeed = 3f;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    /*
	void Update () {
		if(Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(new Vector3(1, 0, 0) * movespeed, ForceMode.Impulse);
        }
	}
    */
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector3 move_dir = collision.relativeVelocity;
            rb.AddForce(move_dir.normalized * launchSpeed, ForceMode.Impulse);
        }
    }
}
