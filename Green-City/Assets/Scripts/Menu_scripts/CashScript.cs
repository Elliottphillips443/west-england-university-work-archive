using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashScript : MonoBehaviour 
{

	public int cash;
	public GameObject CityController;

	void Update () 
	{
		cash = AdaptiveDifficulty.money_current;  // gets the current amount of $$ from the city controller
		GetComponent<Text>().text = "Cash: $ " + cash.ToString ();               // and converts it to string before displaying it
	}
}
