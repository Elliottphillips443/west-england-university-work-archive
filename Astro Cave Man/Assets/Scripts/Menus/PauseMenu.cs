using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private bool paused = false;
    public Canvas myMenu;
    // Use this for initialization
    void Start()
    {
        myMenu = GetComponentInChildren<Canvas>();
        myMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (Input.GetButtonDown("Pause"))
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        paused = true;
        if (paused)
        {
            Time.timeScale = 0f;
        }
        myMenu.gameObject.SetActive(paused);
    }

    public void ResumeGame()
    {
        paused = false;
        if (!paused)
        {
            Time.timeScale = 1f;
        }
        myMenu.gameObject.SetActive(paused);
    }
}