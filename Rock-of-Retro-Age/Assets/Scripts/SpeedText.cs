using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpeedText : MonoBehaviour {
    static public Text speed;
    public GameObject player;
	// Use this for initialization
	void Start () {
        speed = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        //Change Text to player's velocity
        float vel = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        speed.text = vel.ToString(("F1"));
	}
}