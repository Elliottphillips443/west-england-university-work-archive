using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayCharacterSelect : MonoBehaviour {
    public int minPlayerCount = 1;
    public int playerCount = 0;
    private int playerCountOnButton = 0;
    public int numSelected = 0;
    private Text myText;
    public string levelToLoad;

    private void Start()
    {
        myText = GetComponentInChildren<Text>();
    }

    void OnDisable()
    {
        numSelected = 0;        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerMenuSelector>())
        {
            playerCountOnButton++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<PlayerMenuSelector>())
        {
            playerCountOnButton--;
        }
    }

    public void OnClick()
    {
        Debug.Log("PlayerCount = " + playerCount);
        Debug.Log("PlayerCountonButton = " + playerCount);
        Debug.Log("numSelected = " + numSelected);
        if(playerCount >= minPlayerCount && numSelected == playerCount)
        {
            Debug.Log("Loading level");
            SceneManager.LoadScene(levelToLoad);
        }
    }

    public void SetPlayerCount(int count)
    {
        playerCount = count;
    }

    private void Update()
    {
        if(playerCount == numSelected)
        {
            myText.text = "Start Game (" + numSelected.ToString() + "/" + playerCount.ToString() + ")";
        }
        else if (playerCount >= minPlayerCount)
        {
            myText.text = "Play (" + numSelected.ToString() + "/" + playerCount.ToString() + ")";
        }
        else
        {
            myText.text = "Play (" + numSelected.ToString() + "/" + minPlayerCount.ToString() + ")";
        }
    }
}
