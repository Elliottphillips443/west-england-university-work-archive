using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera cam;
    Quaternion start_rotation;

    private void Start()
    {
        start_rotation = this.transform.rotation;
    }

    private void Update()
    {
        transform.rotation = start_rotation * cam.transform.rotation;
    }

}
