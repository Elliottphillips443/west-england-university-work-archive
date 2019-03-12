using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoneText : HUDTextBase {

    private int stoneCount = 0;

    // Update is called once per frame
    void Update()
    {
        stoneCount = myVillage.stone;
        GetComponent<Text>().text = stoneCount.ToString();
    }
}
