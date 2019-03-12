using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrashCollect : MonoBehaviour
{
     private int trashCollected; // how much trash the player has collected

	// Use this for initialization
	void Start ()
    {
        trashCollected = 0; // set trash to 0 on start
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other) // looks to see if anything entered the trigger 
    {
        switch (other.tag)// then does a switch case based on the tag given to the object
        {
            case "Trash":// for anything tagged as trash it will 
                {
                    trashCollected++; // add one to the trash collected count
                    Destroy(other.gameObject);// then delete that object

                    AudioSource crunch = GetComponent<AudioSource>(); //get the audio source
                    if (!crunch.isPlaying)// if the crunch isnt alrdy playing
                    {
                        crunch.Play();// then play
                    }
                    break;
                }

                //case "Bin":// for anything tagged as bin it will
                //{

                //   other.            // will pass the current number of trash collected to bin
                //   trashCollected = 0; // sets the trash count back to 0 
                // }
        }
    }

    int GetTrashCollected() // how much trash the player currently has
    {
        return trashCollected;
    }

    void SetTrashCollected(int newAmount)
    {
        trashCollected = newAmount;
    }

}
