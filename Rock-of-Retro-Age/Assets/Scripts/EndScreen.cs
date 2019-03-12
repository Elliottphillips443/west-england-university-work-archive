using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class EndScreen : MonoBehaviour {

    public Button[] options;
    public Text[] screen;
    public GameObject gate;
    bool display = false;

    // Use this for initialization
    void Start()
    {
        screen = GetComponentsInChildren<Text>();
        options = GetComponentsInChildren<Button>();
        options[0].Select();
        options[0].OnSelect(null);
        //this.gameObject.SetActive(false);  
        HideMenu();
    }
    void HideMenu()
    {
        for (int i = 0; i < screen.Length; i++)
        {
            screen[i].gameObject.SetActive(false);
            
        }
        for (int i = 0; i < options.Length; i++)
        {
            options[i].gameObject.SetActive(false);
        }
    }
    void DisplayMenu()
    {
        for (int i = 0; i < screen.Length; i++)
        {
            screen[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < options.Length; i++)
        {
            options[i].gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gate.GetComponent<GateSingleplayer>().isAlive == false)
        {
            if (!display)
            {
                Time.timeScale = 0.0f;
                options[0].Select();
                options[0].OnSelect(null);
                DisplayMenu();
                display = true;
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
            {
                if (EventSystem.current.currentSelectedGameObject.name == "Button_Replay")
                {
                    SceneManager.LoadScene(1);
                    Time.timeScale = 1.0f;
                }
                else if (EventSystem.current.currentSelectedGameObject.name == "Button_Main_Menu")
                {
                    SceneManager.LoadScene("Main Menu");
                    Time.timeScale = 1.0f;
                }
            }
        }
       
        
    }
}
