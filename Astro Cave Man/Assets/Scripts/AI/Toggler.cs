using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour
{
    private Movement script;
    private Light myLight;

	// Use this for initialization
	void Start ()
    {
        script = GetComponent<Movement>();
        myLight = GetComponent<Light>();
	}

    // Update is called once per frame
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "NPC")
                {
                    if (script.enabled)
                    {
                        this.script.enabled = false;
                        this.myLight.enabled = false;
                    }
                    else if (!script.enabled)
                    {
                        this.script.enabled = true;
                        this.myLight.enabled = true;
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Building")
        {
            this.script.enabled = false;
            this.myLight.enabled = false;
        }
    }
}
