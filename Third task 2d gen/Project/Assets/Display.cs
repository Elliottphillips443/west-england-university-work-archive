using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Display : MonoBehaviour {

    public GameObject information;
    private Text display ;

	// Use this for initialization
	void Start () {
        display = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        string y = information.GetComponent<TileMap>().GetYsize().ToString();
        string x = information.GetComponent<TileMap>().GetXsize().ToString();

        display.text = " Y = " + y + " X = " + x;
	}
}
