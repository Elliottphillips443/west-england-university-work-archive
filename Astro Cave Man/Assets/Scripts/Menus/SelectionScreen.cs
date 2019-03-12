using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectionScreen : MonoBehaviour {
    public string[] controllers;
    public int nextId = 1;
    public List<int> LeftIds = null;
    public List<GameObject> players = null;
    public int numofplayers = 0;
    public GameObject playerPrefab;
    public GameObject playButton;
    public Button[] childButtons;
    private Color[] playerColours = { Color.red, Color.cyan, Color.green, Color.yellow };

	// Use this for initialization
	void Awake () {
        playerPrefab = Resources.Load("Prefabs/PlayerMenuSelector") as GameObject;
        childButtons = GetComponentsInChildren<Button>();
        LeftIds = new List<int>();
        controllers = Input.GetJoystickNames();



    }
	
    void OnEnable()
    {
        if (!playButton)
        {
            playButton = GetComponentInChildren<PlayCharacterSelect>().gameObject;
        }
       // playButton.GetComponent<PlayCharacterSelect>().SetPlayerCount(controllers.Length);
        players = new List<GameObject>();
        LeftIds = new List<int>();
        /*
        if(controllers.Length > 0)
        {
            //players = new List<GameObject>();
            for(int i = 0; i< controllers.Length;i++)
            {
                GameObject newPlayer = Instantiate(playerPrefab, transform, false);
                newPlayer.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                newPlayer.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
                newPlayer.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
                newPlayer.transform.position = new Vector2(Screen.width * ((float)(i + 1) / 6), Screen.height / 2);
                newPlayer.GetComponentInChildren<Text>().text = "P" + (i + 1);
                newPlayer.GetComponent<Image>().color = Random.ColorHSV();
                newPlayer.GetComponent<PlayerMenuSelector>().playerNumber = i + 1;
                players.Add(newPlayer);
            }
        }
        //comment this out for final build
        
        if (numofplayers > 0)
        {
            //players = new List<GameObject>();         
            for (int i = 0; i < numofplayers; i++)
            {
                GameObject newPlayer = Instantiate(playerPrefab, transform, false);
                newPlayer.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                newPlayer.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
                newPlayer.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
                newPlayer.GetComponent<BoxCollider2D>().offset = new Vector2(10f, 10f);
                newPlayer.transform.position = new Vector2(Screen.width * ((float)(i+1)/6), Screen.height / 2);
                newPlayer.GetComponentInChildren<Text>().text = "P" + (i + 1);
                newPlayer.GetComponent<Image>().color = Random.ColorHSV();
                newPlayer.GetComponent<PlayerMenuSelector>().playerNumber = i + 1;
                players.Add(newPlayer);
            }
            
        }*/
    }

    void OnDisable()
    {
        //reset the player list
        if (players != null)
        {
            if (players.Count > 0)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    Destroy(players[i]);
                }
            }
        }
        nextId = 1;
        for(int i = 0; i < childButtons.Length;i++)
        {
            /*
            ColorBlock temp = childButtons[i].colors;
            temp.normalColor = Color.white;
            childButtons[i].colors = temp;
            */
            childButtons[i].interactable = true;
        }
    }

    public void JoinPlayer(string controllerName, int controllerNum)
    {
        if (LeftIds.Count > 0)
        {
            LeftIds.Sort();
            nextId = LeftIds[0];
            LeftIds.RemoveAt(0);
        }
        GameObject newPlayer = Instantiate(playerPrefab, transform, false);
        newPlayer.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        newPlayer.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        newPlayer.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        newPlayer.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        newPlayer.GetComponentInChildren<Text>().text = "P" + (nextId);
        newPlayer.GetComponent<Image>().color = playerColours[nextId - 1];
        newPlayer.GetComponent<PlayerMenuSelector>().playerNumber = nextId;
        newPlayer.GetComponent<PlayerMenuSelector>().controller = controllerName;
        newPlayer.GetComponent<PlayerMenuSelector>().controllerNumber = controllerNum;
        players.Add(newPlayer);

        PlayerInfoStruct tempPlayer;
		tempPlayer.playerNumber = controllerNum;
        Debug.Log("Player ID " + controllerNum + " assigned");
		tempPlayer.sign = STAR_SIGN.NULL;
		PlayerInformation.addToList(tempPlayer);
		
		if(LeftIds.Count == 0)
		{
			nextId = players.Count + 1;
		}
        if(LeftIds.Count == 0)
        {
            nextId = players.Count + 1;
        }
        playButton.GetComponent<PlayCharacterSelect>().playerCount++;
    }

    public void LeavePlayer(GameObject player)
    {
        int index = players.IndexOf(player);
        LeftIds.Add(player.GetComponent<PlayerMenuSelector>().playerNumber);
        if (LeftIds.Count > 0)
        {
            LeftIds.Sort();
            nextId = LeftIds[0];
        }
        Destroy(players[index]);
        players.RemoveAt(index);
		PlayerInformation.removeFromList(index);
        playButton.GetComponent<PlayCharacterSelect>().playerCount--;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Submit 1"))
        {
            bool inGame = false;
            for(int i = 0; i < players.Count;i++)
            {
                if(players[i].GetComponent<PlayerMenuSelector>().controller == "c1")
                {
                    inGame = true;
                    break;
                }
            }
            if(!inGame)
            {
                JoinPlayer("c1", 1);
            }
        }
        if (Input.GetButtonDown("Submit 2"))
        {
            bool inGame = false;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetComponent<PlayerMenuSelector>().controller == "c2")
                {
                    inGame = true;
                    break;
                }
            }
            if (!inGame)
            {
                JoinPlayer("c2", 2);
            }
        }
        if (Input.GetButtonDown("Submit 3"))
        {
            bool inGame = false;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetComponent<PlayerMenuSelector>().controller == "c3")
                {
                    inGame = true;
                    break;
                }
            }
            if (!inGame)
            {
                JoinPlayer("c3", 3);
            }
        }
        if (Input.GetButtonDown("Submit 4"))
        {
            bool inGame = false;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetComponent<PlayerMenuSelector>().controller == "c4")
                {
                    inGame = true;
                    break;
                }
            }
            if (!inGame)
            {
                JoinPlayer("c4", 4);
            }
        }

        int count = 0;
        for(int i = 0; i < players.Count;i++)
        {
            if(players[i].GetComponent<PlayerMenuSelector>().selectedCharacter)
            {
                count++;
            }
        }
        
    }
}
