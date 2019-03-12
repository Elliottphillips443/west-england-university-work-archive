using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FaithText : HUDTextBase {

    private int faithCount = 0;

    // Update is called once per frame
    void Update()
    {
        faithCount = myVillage.wood;
        GetComponent<Text>().text = faithCount.ToString();
    }
}
