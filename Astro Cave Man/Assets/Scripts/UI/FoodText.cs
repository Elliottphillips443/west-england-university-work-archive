using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FoodText : HUDTextBase {

    private int foodCount = 0;

    // Update is called once per frame
    void Update()
    {
        foodCount = myVillage.food;
        GetComponent<Text>().text = foodCount.ToString();
    }
}
