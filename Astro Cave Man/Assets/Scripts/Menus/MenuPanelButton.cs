using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuPanelButton : ButtonBaseClass {
    public string MenuName;
    public GameObject menu;

    protected override void OnClickTask()
    {
        menu.GetComponent<Menu>().changePanel(MenuName);
        Debug.Log("clicked");
    }

}
