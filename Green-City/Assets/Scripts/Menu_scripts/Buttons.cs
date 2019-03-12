using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{
   public bool start;                 // assing this as true in editor for the start button
   public bool quit;                  // assing this as true in editor for the quit button
   public GameObject CityController;

    void OnMouseUp()
    {
        if (start)
        {
            CityController.GetComponent<CityGenerator>().m_on_menu = false;     // not sure if this works... but it's set to "false" in the CityControllers CityGenerators Inspector in Game scene just in case
            SceneManager.LoadScene("Game", LoadSceneMode.Single);    // loads Game scene (rename if necessary) and closes any other scenes (change to .Addictive if u want to keep the menu scene open)
        }

        if (quit)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
