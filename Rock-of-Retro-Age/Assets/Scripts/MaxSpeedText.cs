using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MaxSpeedText : MonoBehaviour {
    public float max_speed;
    public Text myText;
	// Use this for initialization
	void Start () {
        max_speed = 0;
        myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        max_speed = BoudlerController.max_velocity;
        myText.text = max_speed.ToString("F1");
	}
}
