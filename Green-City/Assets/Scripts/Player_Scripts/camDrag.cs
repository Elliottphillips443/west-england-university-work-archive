using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camDrag : MonoBehaviour
{

    private Vector3 startMousePos;
    private Vector3 nowMousePos;
    private bool cameraMovementDisabled = false;

    void Update()
    {
        if (!cameraMovementDisabled)
        {
            if (Input.GetMouseButtonDown(0))                                            // when you click the mouse button down
            {
                startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // get the location of the mouse
            }

            if (Input.GetMouseButton(0))                                                // as you hold down the mouse button
            {
                nowMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      // keep updating on the mouse location
                transform.position = transform.position + new Vector3(startMousePos.x - nowMousePos.x, 0, startMousePos.z - nowMousePos.z);
            }
        }// move the camera in X (side to side) and Z (up/down) axis
    }

    public bool GetCameraMovementDisabled() // get weather or not the camera movement is disabled
    {
        return cameraMovementDisabled;
    }

    public void SetCameraMovementDisabled(bool setBool)// set the camer movement to true or false
    {
        cameraMovementDisabled = setBool;
    }
}