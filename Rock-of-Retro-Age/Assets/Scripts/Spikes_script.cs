using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes_script : MonoBehaviour
{

    public GameObject spikeSound;

    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {

                spikeSound.GetComponent<AudioSource>().Play();

                col.gameObject.GetComponent<BoudlerController>().health -= 0.1f;

                col.attachedRigidbody.velocity *= 0.1f;
                col.gameObject.GetComponent<BoudlerController>().direction *= 0.1f;

                this.gameObject.SetActive(false);
        }
    }
}
