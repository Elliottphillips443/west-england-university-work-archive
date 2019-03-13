using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

    int money = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMoney(int newMoney)
    {
        money += newMoney;
    }

    public int GetMoney()
    {
       return money;
    }
}
