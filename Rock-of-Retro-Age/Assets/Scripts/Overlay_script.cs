using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay_script : MonoBehaviour
{
    Transform smokePos;
    private float sec = 5f;

    // Use this for initialization
    void Start ()
    {
        smokePos = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(smokePos.position.x, smokePos.position.y, transform.position.z);

        if (gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
    {

        yield return new WaitForSeconds(sec);

        gameObject.SetActive(false);

    }
}
