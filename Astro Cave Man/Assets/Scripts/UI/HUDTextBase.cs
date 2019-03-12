using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDTextBase : MonoBehaviour {

    public Village myVillage = null;

	// Use this for initialization
    public void SetVillage(Village set)
    {
        myVillage = set;
    }
}
