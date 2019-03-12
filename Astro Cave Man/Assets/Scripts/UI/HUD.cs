using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour {
    public HUDTextBase[] textElements;
    public Village trackedVillage;
	// Use this for initialization

	void Start ()
    {
        textElements = GetComponentsInChildren<HUDTextBase>();
        for(int i = 0; i<textElements.Length;i++)
        {
            textElements[i].myVillage = trackedVillage;
            //textElements[i].GetComponent<Text>();
        }
        /*
        text order:
        populaiton
        faith
        food
        wood
        stone
        barracks
        */
	}
	
	// Update is called once per frame
	void Update () {

	}
}
