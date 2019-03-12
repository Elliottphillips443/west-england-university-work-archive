using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public Canvas myCanvas;
    public bool game_over = false;
    private bool screen_enabled = false;

	void Start ()
    {
        myCanvas = GetComponentInChildren<Canvas>();
        myCanvas.gameObject.SetActive(false);
	}

	void Update ()
    {
		if(game_over && !screen_enabled)
        {
            myCanvas.gameObject.SetActive(true);
        }
	}

    public void EndGame(Sprite winnerPic)
    {
        myCanvas.GetComponentInChildren<EndPanel>().winPic = winnerPic;
        game_over = true;
    }
}
