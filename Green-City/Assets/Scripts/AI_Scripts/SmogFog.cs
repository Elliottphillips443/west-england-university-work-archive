using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogFog : MonoBehaviour {

    public CanvasRenderer canv;
    public CityGenerator city;

    public float opacity;

	// Use this for initialization
	void Start ()
    {
        opacity = canv.GetAlpha();
	}
	
	// Update is called once per frame
	void Update ()
    {
        opacity = 1 - city.GetComponent<AdaptiveDifficulty>().satisfaction;
        canv.SetAlpha(opacity);
	}
}