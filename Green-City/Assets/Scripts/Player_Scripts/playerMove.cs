using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerMove : MonoBehaviour
{
    private Vector3 startMousePos;
    private Vector3 nowMousePos;
    private Vector3 cameraTempPos;
    private bool playerMovemetDisabled = false;
    private NavMeshAgent agent;

    /*

        if the player isnt moving its cuz the camera is to high

    */
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (!playerMovemetDisabled) // if the players movement isnt disabled then do
        {
            if (Input.GetMouseButtonDown(0))                                            // when you click the mouse button down
            {

               // startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // get the location of the mouse
                cameraTempPos = Camera.main.transform.position; // store the start camera pos
            }

            if (Input.GetMouseButtonUp(0))
            {

                if (Camera.main.transform.position == cameraTempPos)  // if the new camera pos is the same as the temp pos then
                {
                    RaycastHit hit;

                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                    {
                        if (hit.transform.tag == "Floor")// the u hit the floor with ray cast
                        {
                            SetNewPlayerPos(hit.point); // set the player to move to ray hit pos
                        }
                    }
                }
            }
        }
    }

    public bool GetPlayerMovementDisabled() // get weather or not the player movement is disabled
    {
        return playerMovemetDisabled;
    }

    public void SetPlayerMovementDisabled(bool setBool)// set the players movement
    {
        playerMovemetDisabled = setBool;
    }

    public void SetNewPlayerPos(Vector3 pos)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = pos;  // move to the pos in function
    }


}

