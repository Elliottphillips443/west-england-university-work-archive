using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building_UI : MonoBehaviour
{
    Building building;
    List<Image> sprites = new List<Image>();
    List<Text> text = new List<Text>();



    private void Start()
    {
        if(!(building = this.GetComponent<Building>()))
        {
            print("You have not attached this to a valid object");
            return;
        }
    }



    private void resourceIcon(Building.BUILDINGTYPE type)
    {

    }



    private void healthIcon(Building.BUILDINGTYPE type)
    {

    }



    private void workerIcon(Building.BUILDINGTYPE type)
    {

    }
}
