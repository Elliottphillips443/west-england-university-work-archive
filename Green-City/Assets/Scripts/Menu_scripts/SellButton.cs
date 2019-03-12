using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class SellButton : MonoBehaviour {
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
        TowerSelection.SetSell();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventSystem.current.SetSelectedGameObject(myButton.gameObject);
        }
    }
}
