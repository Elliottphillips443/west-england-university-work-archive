using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PopText : HUDTextBase {

    private int popCount = 0;

    // Update is called once per frame
    void Update()
    {
        popCount = myVillage.population;
        GetComponent<Text>().text = popCount.ToString();
    }
}
