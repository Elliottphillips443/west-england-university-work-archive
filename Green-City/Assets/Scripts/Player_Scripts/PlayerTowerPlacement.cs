using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTowerPlacement : MonoBehaviour
{

    public GameObject camera;
    public GameObject player;
    public Button binButton;

    // Use this for initialization
    void Start()
    {
        binButton = GetComponent<Button>();
        binButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void TaskOnClick()
    {
        if (!camera.GetComponent<camDrag>().GetCameraMovementDisabled())
        {
            camera.GetComponent<camDrag>().SetCameraMovementDisabled(true);
        }
        else if (camera.GetComponent<camDrag>().GetCameraMovementDisabled())
        {
            camera.GetComponent<camDrag>().SetCameraMovementDisabled(false);
        }

        if (!player.GetComponent<playerMove>().GetPlayerMovementDisabled())
        {
            player.GetComponent<playerMove>().SetPlayerMovementDisabled(true);
        }
        else if (player.GetComponent<playerMove>().GetPlayerMovementDisabled())
        {
            player.GetComponent<playerMove>().SetPlayerMovementDisabled(false);
           // player.GetComponent<playerMove>().SetNewPlayerPos(player.transform.position);
        }
    }
}