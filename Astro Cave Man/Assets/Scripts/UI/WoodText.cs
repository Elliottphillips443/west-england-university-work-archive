using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WoodText : HUDTextBase {

    private int woodCount = 0;
	
	// Update is called once per frame
	void Update () {
        woodCount = myVillage.wood;
        GetComponent<Text>().text = woodCount.ToString();
	}
}
