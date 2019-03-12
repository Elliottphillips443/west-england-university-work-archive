using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public GameObject[] teleporterDestination;
    public GameObject endDestination;
    int index;

    // Use this for initialization
    void Start()
    { 
        index = Random.Range(0, teleporterDestination.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<BoudlerController>().timesTeleported < teleporterDestination.Length)
            {
                CameraController cameraController;
                if (!(cameraController = other.GetComponent<CameraController>()))
                {
                    cameraController = other.transform.gameObject.AddComponent<CameraController>();
                    cameraController.AddTarget(other.transform);
                }

                Vector3 difference = Camera.main.transform.position - other.transform.position;

                other.transform.position = teleporterDestination[index].transform.position
                    + (other.transform.position - this.transform.position);
                index = Random.Range(0, teleporterDestination.Length);
                other.GetComponent<BoudlerController>().timesTeleported++;

                Camera.main.transform.position = other.transform.position + difference;          
            }   

            else
            {
                other.transform.position = endDestination.transform.position
                    + (other.transform.position - this.transform.position);
            }
        }

    }
}

