using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour
{

    public float nimZoom;
    public float maxZoom;
    public float sensitivity;

    private Camera myCamera;

    void Start ()
    {
        myCamera = GetComponent<Camera>();
    }


	void Update ()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            myCamera.orthographicSize += sensitivity;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            myCamera.orthographicSize -= sensitivity;
        }
        myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, nimZoom, maxZoom);
    }
}
