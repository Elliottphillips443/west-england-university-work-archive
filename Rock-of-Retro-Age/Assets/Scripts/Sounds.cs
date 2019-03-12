using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sounds : MonoBehaviour
{

    AudioSource audioSource;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
    }

    void Update()
    {
        GetComponent<AudioSource>().volume = (player.GetComponent<BoudlerController>().boulder.velocity.magnitude * 0.1f);

		if (GetComponent<AudioSource> ().volume != 0 && !GetComponent<AudioSource> ().isPlaying)
		{
			GetComponent<AudioSource> ().Play ();
		}
	}

}