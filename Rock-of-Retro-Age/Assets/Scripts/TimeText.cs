using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeText : MonoBehaviour {
    static public Text myText;
    public float timer = 0;
    private bool timerOn = false;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        StartTimer();
	}
	
    void StartTimer()
    {
        timerOn = true;
    }

    void StopTimer()
    {
        timerOn = false;
    }

    void ResetTimer()
    {
        timerOn = false;
        timer = 0;
        myText.text = "00:00";
        timerOn = true;
    }

	// Update is called once per frame
	void Update () {
        if (timerOn)
        {
            //Add to timer and set to 00:00 format
            timer += Time.deltaTime;
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = Mathf.Floor(timer % 60).ToString("00");
            myText.text = minutes + ":" + seconds;
        } 
	}
}
