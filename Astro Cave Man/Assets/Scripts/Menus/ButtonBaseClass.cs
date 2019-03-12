using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonBaseClass : MonoBehaviour {

    public Button myButton;
    
	// Use this for initialization
	void Start () {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClickTask);
    }
	
	virtual protected void OnClickTask()
    {
        Debug.Log("default on click");
    }
}
