using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMenuSelector : MonoBehaviour {
    public float limitTop;
    public float limitLeft;
    public float limitRight;
    public float limitBottom;
    public int playerNumber;
    public int controllerNumber;
    public string controller;

    public GameObject currentOverlap = null;
    public GameObject selectedCharacter = null;
    public Canvas myCanvas;
    private PlayCharacterSelect startButton;
    private RectTransform myRect;

	STAR_SIGN mySign;

	// Use this for initialization
	void Start () {
        myCanvas = transform.parent.transform.parent.GetComponent<Canvas>();
        startButton = transform.parent.GetComponentInChildren<PlayCharacterSelect>();
        limitTop = Screen.height; 
        limitLeft = 0;
        limitRight = Screen.width;
        limitBottom = 0;
        myRect = GetComponent<RectTransform>();
	}

    // Update is called once per frame
    void Update()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
        GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<RectTransform>().rect.width / 2, GetComponent<RectTransform>().rect.height / 2);
        //select character
        if (Input.GetButtonDown("Submit " + controllerNumber))
        {
            if(currentOverlap && currentOverlap.GetComponent<Button>().interactable)
            {
              
                //select new character
                if (currentOverlap.tag == "CharacterButton")
                {
                    if (selectedCharacter)
                    {
						
						selectedCharacter.GetComponent<Button>().interactable = true;
                        startButton.numSelected--;
                    }
                    selectedCharacter = currentOverlap;
                    var colours = selectedCharacter.GetComponent<Button>().colors;
                    colours.disabledColor = GetComponent<Image>().color;
                    selectedCharacter.GetComponent<Button>().colors = colours;
                    selectedCharacter.GetComponent<Button>().interactable = false;
                    startButton.numSelected++;

                    PlayerInfoStruct tempPlayer;
					tempPlayer = PlayerInformation.getListItem(playerNumber - 1);


					mySign = selectedCharacter.GetComponent<StarSignButton> ().getSign();
					tempPlayer.sign = mySign;
					PlayerInformation.overwriteListItem(tempPlayer, playerNumber - 1);

                }
                //select back button
                else if(currentOverlap.name == "BackButton")
                {
                    myCanvas.GetComponent<Menu>().changePanel("MainPanel");
                }
                //select play button
                else if(currentOverlap.name == "PlayButton")
                {
                    currentOverlap.GetComponent<PlayCharacterSelect>().OnClick();
                }
            }
        }

        //deselect character
        //need to make joystick specific
        //if(Input.GetButtonDown("joystick " + playerNumber + " Cancel")
        if(Input.GetButtonDown("Cancel " + controllerNumber))
        {
            if(selectedCharacter)
            {
                selectedCharacter.GetComponent<Button>().interactable = true;
                selectedCharacter = null;
                startButton.numSelected--;

                PlayerInfoStruct tempPlayer;
				tempPlayer = PlayerInformation.getListItem(playerNumber - 1);

                
                mySign = STAR_SIGN.NULL;
				tempPlayer.sign = mySign;
                PlayerInformation.overwriteListItem(tempPlayer, playerNumber - 1);
            }
            else
            {
                transform.parent.GetComponent<SelectionScreen>().LeavePlayer(this.gameObject);              
            }
        }

        Vector3 movement = new Vector3(0, 0, 0);
        movement.x += Input.GetAxis ("Horizontal Joystick " + controllerNumber) * 5;
        movement.y += Input.GetAxis("Vertical Joystick " + controllerNumber) * 5;

        transform.position += movement;

        if (transform.position.x + (myRect.rect.width * myCanvas.scaleFactor) >= limitRight)
        {
            transform.position = new Vector3(limitRight - (myRect.rect.width * myCanvas.scaleFactor), transform.position.y, transform.position.z);
        }
        if (transform.position.y + (myRect.rect.height * myCanvas.scaleFactor) >= limitTop)
        {
            transform.position = new Vector3(transform.position.x, limitTop - (myRect.rect.height * myCanvas.scaleFactor), transform.position.z);
        }
        if (transform.position.x - transform.localScale.x <= limitLeft)
        {
            transform.position = new Vector3(limitLeft, transform.position.y, transform.position.z);
        }
        if (transform.position.y - transform.localScale.y  <= limitBottom)
        {
            transform.position = new Vector3(transform.position.x, limitBottom, transform.position.z);
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Button>())
        {
            currentOverlap = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == currentOverlap)
        {
            currentOverlap = null;
        }
    }
}
