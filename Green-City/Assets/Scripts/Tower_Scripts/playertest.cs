using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertest : MonoBehaviour {
    Rigidbody rb;
    [SerializeField]
    float movespeed = 3f;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(new Vector3(1, 0, 0) * movespeed, ForceMode.Impulse);
        }
    }
}
