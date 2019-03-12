using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReturnToMenuButton : ButtonBaseClass {
    //change this string to the name of the main menu scene
    public string menuName = "Main_Menu";
	
	protected override void OnClickTask()
    {
        if (Application.CanStreamedLevelBeLoaded(menuName))
        {
            SceneManager.LoadScene(menuName);
        }
        else
        {
            Debug.Log("Error: Menu scene name doesn't exist");
        }
    }
}
