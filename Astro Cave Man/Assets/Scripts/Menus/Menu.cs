using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Menu : MonoBehaviour{

    public List<GameObject> panels;
    public GameObject currentPanel;
    public GameObject previousPanel;
	// Use this for initialization
	void Start () {
        foreach(Transform child in transform)
        {
            panels.Add(child.gameObject);
            if(child.gameObject.activeSelf)
            {
                currentPanel = child.gameObject;
            }
        }
	}

    public void changePanel(string newPanel)
    {
        for(int i = 0; i < panels.Count;i++)
        {
            if(panels[i].name == newPanel)
            {
                previousPanel = currentPanel;
                currentPanel = panels[i];
                previousPanel.SetActive(false);
                currentPanel.SetActive(true);
                break;
            }
        }
    }
}
