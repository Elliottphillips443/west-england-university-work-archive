using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour 
{
	public float satisfaction = 1;
	public GameObject CityController;
	Vector3 pos;

	void Start ()
	{
		pos = new Vector3 (300, -20, 0);                                                        // sets the initial position of the arrow (in case it gets moved in the editor by accident)
	}

	void Update () 
	{
		satisfaction = CityController.GetComponent<AdaptiveDifficulty>().satisfaction-0.5f;     // "satisfaction" is set between 0 and 1 so I need reduce it so that the arrow can go into "negative"
		satisfaction = satisfaction * 600.0f;                                                   // we then multiply it by 600 (the lenght of the bar)

		pos = new Vector3 (satisfaction, -10, 0);                                               // and we reduce (move) the arrow on the bar by 10 units for every negative 0.1 citizen satisfaction
		GetComponent<RectTransform> ().transform.localPosition = pos;
	}
}
