using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    private bool paused = false;
    private Button[] options;
    public Text pausedText;
    public Canvas gameplay_gui;

	// Use this for initialization
	void Start () {
        options = GetComponentsInChildren<Button>();
        for(int i = 0; i < options.Length; i++)
        {
            options[i].gameObject.SetActive(false);
        }
        pausedText.gameObject.SetActive(false);
	}
    void DisplayMenu()
    {
        for(int i =0; i < options.Length;i++)
        {
            options[i].gameObject.SetActive(true);
        }
        pausedText.gameObject.SetActive(true);
    }

    void HideMenu()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].gameObject.SetActive(false);
        }
        pausedText.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        if (paused)
        {
            //"Fire1" = A button, "Cancel" = B button and "JoyStickButton 7" = Start button (xbox controller)
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                HideMenu();
                gameplay_gui.gameObject.SetActive(true);
                paused = false;
                Time.timeScale = 1.0f;
            }
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
            {
                if (EventSystem.current.currentSelectedGameObject.name == "Button_Resume")
                {
                    HideMenu();
                    gameplay_gui.gameObject.SetActive(true);
                    paused = false;
                    Time.timeScale = 1.0f;
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Button_Restart")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    Time.timeScale = 1.0f;
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Button_Main_Menu")
                {
                    SceneManager.LoadScene("Main Menu");
                    Time.timeScale = 1.0f;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                paused = true;
                Time.timeScale = 0.0f;
                gameplay_gui.gameObject.SetActive(false);
                options[0].Select();
                options[0].OnSelect(null);
                DisplayMenu();
            }
        }
	}
}