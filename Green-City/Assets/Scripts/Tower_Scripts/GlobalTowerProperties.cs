using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TowerProperties
{
    public float range;  //how far is its effect reaching
    public float rateOfFire; //how fast it pulls in rubbish
    public int cost;   //how much the player must pay to place
    public int forNextUpgrade;    //resource requirement to upgrade
    public string spriteName;  //sprite of the tower, changes at certain levels
}

[System.Serializable]
public class PropertiesList
{
    public List<TowerProperties> propertiesList = new List<TowerProperties>();
}

public enum TowerTypes
{
    NONE = 0,
    SELL = 1,
    BIN = 2,
    RECYCLING = 3
}