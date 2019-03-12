using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    private AudioSource happySoundTrack;
    private AudioSource sadSoundTrack;
    public GameObject cityController;


    // Use this for initialization
    void Start () {
        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();

        happySoundTrack = allMyAudioSources[0];
        sadSoundTrack = allMyAudioSources[1];
        sadSoundTrack.volume = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        happySoundTrack.volume = cityController.GetComponent<AdaptiveDifficulty>().satisfaction;

       sadSoundTrack.volume = 1 - cityController.GetComponent<AdaptiveDifficulty>().satisfaction;

    }
}
