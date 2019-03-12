using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
	public float flashingFrequency = 0.50f;
    public Color my_colour = new Color(26.0f, 120.0f, 34.0f, 255.0f);
    public Color my_otherColour = new Color(0.0f, 190.0f, 0.0f, 255.0f);

    void Start ()
    {
		flashingFrequency = flashingFrequency / 2;                      // needed to divide this by 2 to avoid few issues (not an ideal fix so try not to change the flashingFrequency if possible)
        GetComponent<TextMesh>().fontStyle = FontStyle.Italic;
        GetComponent<TextMesh>().color = my_colour;
    }

    void OnMouseEnter()
    {
        GetComponent<TextMesh>().fontStyle = FontStyle.Bold;            // changes font style to Bold when you hover your mouse over the text
		InvokeRepeating("FlashOtherColour", 0.1f, flashingFrequency);   // starts repeating "FlashOtherColour" function after 0.1sec of user putting his mouse over the text
        InvokeRepeating("FlashMyColour", 0.2f, flashingFrequency);      // "FlashMyColour" function starts 0.1 seconds after the "FlashOtherColour" function
    }

    void FlashOtherColour()
    {
        GetComponent<TextMesh>().color = my_otherColour;
    }

	void FlashMyColour()
    {
        GetComponent<TextMesh>().color = my_colour;
    }

    void OnMouseExit()
    {
        CancelInvoke();
        GetComponent<TextMesh>().fontStyle = FontStyle.Italic;        // changes font style to Italic when your mouse leaves the text collider
        GetComponent<TextMesh>().color = my_colour;
    }
}