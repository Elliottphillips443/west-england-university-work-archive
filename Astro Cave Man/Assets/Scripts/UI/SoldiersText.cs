using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoldiersText : HUDTextBase {

    private int soldierCount = 0;

    // Update is called once per frame
    void Update()
    {
        soldierCount = 0;
        for (int i = 0; i < myVillage.population; i++)
        {
            if(myVillage.villagers[i].job == JOB.SOLDIER)
            {
                soldierCount++;
            }
        }
        GetComponent<Text>().text = soldierCount.ToString();
    }
}
