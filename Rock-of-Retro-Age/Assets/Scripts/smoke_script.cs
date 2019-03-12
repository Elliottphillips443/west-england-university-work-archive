using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke_script : MonoBehaviour
{
    public GameObject target;

    void Start ()
    {
       
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            target.SetActive(true);
        }
    }
}
