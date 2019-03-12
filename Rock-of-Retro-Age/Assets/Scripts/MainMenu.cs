using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public Button[] options;
	// Use this for initialization
	void Start () {
        options = GetComponentsInChildren<Button>();
        options[0].Select();
        options[0].OnSelect(null);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            if(EventSystem.current.currentSelectedGameObject.name == "Button_1P")
            {
                SceneManager.LoadScene(1);
            }
            else if(EventSystem.current.currentSelectedGameObject.name == "Button_2P")
            {

            }
            else if(EventSystem.current.currentSelectedGameObject.name == "Button_Scores")
            {

            }
            else if(EventSystem.current.currentSelectedGameObject.name == "Button_Quit")
            {
                Application.Quit();
            }
        }
	}
}
