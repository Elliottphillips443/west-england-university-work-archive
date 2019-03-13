using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour {

    string resource;
    int amout;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    void CreateTransporter(float x, float y)
    {
        Instantiate(this, new Vector2(x, y), Quaternion.identity);
    }
    */

    public string GetResource()
    {
        return resource;
    }

    public void SetResource(string newResource)
    {
         resource = newResource;
    }

    public int GetAmout()
    {
        return amout;
    }

    public void SetAmout(string newAmout)
    {
        resource = newAmout;
    }

}
