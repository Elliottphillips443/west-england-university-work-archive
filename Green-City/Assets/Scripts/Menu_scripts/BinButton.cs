using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class BinButton : MonoBehaviour {
    private Button myButton;

    // Use this for initialization
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClickTask);
    }

    virtual protected void OnClickTask()
    {
        EventSystem.current.SetSelectedGameObject(myButton.gameObject);
        TowerSelection.SetBins();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EventSystem.current.SetSelectedGameObject(myButton.gameObject);
        }
    }
}
